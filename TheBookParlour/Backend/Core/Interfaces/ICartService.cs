using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartItemResponse?> AddCartItemAsync(int userId, AddCartItemRequest request);
        Task<bool> DeleteCartItemAsync(int id);
        Task<CartResponse?> GetCartByUserIdAsync(int userId);
    }
}
