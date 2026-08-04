using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.Interfaces
{
    public interface ICartRepo
    {
        Task<List<CartItem>> GetCartItemsAsync(int id);
        Task<CartItem> AddCartItemAsync(CartItem item);
        Task<bool> DeleteCartItemAsync(int id);
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(Cart cart);
    }
}
