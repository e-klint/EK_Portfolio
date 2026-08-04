using Microsoft.EntityFrameworkCore;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Data.Repos
{
    public class CartRepo : ICartRepo
    {
        private readonly BookshopContext _context;

        public CartRepo(BookshopContext context)
        {
            _context = context;
        }
        public async Task<CartItem> AddCartItemAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var cartItemToDelete = await _context.CartItems.FindAsync(id);

            if (cartItemToDelete is null)
                return false;

            _context.Remove(cartItemToDelete);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int id)
        {
            return await _context.CartItems
                .Where(i => i.CartId == id)
                .Include(i => i.Book)
                .ToListAsync();
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
