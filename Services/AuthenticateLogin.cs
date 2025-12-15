using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Services
{
    public class AuthenticateLogin
    {
        private readonly AppDbContext _context;

        public AuthenticateLogin(AppDbContext context)
        {
            _context = context;
        }

        public User? ValidateUser(string username, string password)
        {
            // 1️⃣ Find user by username
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            // 2️⃣ Verify password using bcrypt
            bool isValid = BCrypt.Net.BCrypt.Verify(
                password,
                user.Password
            );

            // 3️⃣ Return user if valid
            return isValid ? user : null;
        }
    }
}
