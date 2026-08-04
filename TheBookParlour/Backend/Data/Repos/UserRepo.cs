using Microsoft.EntityFrameworkCore;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Data.Repos

{
    public class UserRepo : IUserRepo
    {
        private readonly BookshopContext _context;

        public UserRepo(BookshopContext context)
        {
            _context = context;
        }


        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username); //Returnerar användare
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);  
        }
    }
}
