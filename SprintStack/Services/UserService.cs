using Microsoft.EntityFrameworkCore;
using SprintStack.Data;

using SprintStack.DTOs;

namespace SprintStack.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        
        public UserService(AppDbContext context) => _context = context;

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive);
        }

        public async Task<User> RegisterUserAsync(RegisterRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                IsActive = true,
                RoleId = 2
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}