using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

    
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSault);

            var calculatedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));   

            for(int i=0; i< calculatedHash.Length; i++){
                if(calculatedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
                
            }    

            return new UserDTO {
                UserName = user.Username,
                Token = _tokenService.CreateToken(user)
            };         
        }
        

        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO){
            
            if(await UserExists(registerDTO.Username)) return BadRequest("User already exists");

            using var hmac = new HMACSHA512();

            var user = new AppUser{
                Username= registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSault = hmac.Key
            };

            _context.users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDTO{
                UserName = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string uesrname){
            return await _context.users.AnyAsync(x => x.Username == uesrname.ToLower());
        }
    }
}