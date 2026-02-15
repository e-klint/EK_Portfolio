using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBankApp.Data.Repos
{
    public class UserRepo : IUserRepo 
    {    
        private readonly BankAppDataContext _context;

        public UserRepo(BankAppDataContext bankAppDbContext)
        {
            _context = bankAppDbContext;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(int userId) 
        { 
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}

