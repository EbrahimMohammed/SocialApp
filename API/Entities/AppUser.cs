namespace API.Entities
{
    public class AppUser
    {
        
        public int Id { get; set; }

        public string Username { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSault { get; set; }

    }
}