using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController: BaseAPIController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return await _context.users.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.users.FindAsync(id);
        }
    }
}