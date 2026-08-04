using Mapster;
using TheBookParlour.Core.Helpers;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;
using TheBookParlour.Data.Repos;

namespace TheBookParlour.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IBookRepo _bookRepo;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepo cartRepo, IBookRepo bookRepo, ILogger<CartService> logger)
        {
            _cartRepo = cartRepo;
            _bookRepo = bookRepo;
            _logger = logger;
        }

        public async Task<CartItemResponse?> AddCartItemAsync(int userId, AddCartItemRequest request)
        {
            // Hämta rätt varukorg, skapa ny om den inte finns. 
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);

            if (cart is null)
            {
                _logger.LogWarning("Cart for user {UserId} was not found", userId);
                cart = await _cartRepo.CreateCartAsync(new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Hämta boken för att få rätt pris
            var book = await _bookRepo.GetBookAsync(request.BookId);

            if (book is null)
            {
                _logger.LogWarning("Book with id {BookId} was not found", request.BookId);
                return null;
            }

            // Bygg CartItem
            var item = new CartItem
            {
                CartId = cart.CartId,
                BookId = request.BookId,
                Quantity = request.Quantity,
                Price = book.Price
            };

            var addedItem = await _cartRepo.AddCartItemAsync(item);
            _logger.LogInformation("Item {CartItemId} was added to cart", addedItem.CartItemId);

            return addedItem.Adapt<CartItemResponse>();
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var isDeleted = await _cartRepo.DeleteCartItemAsync(id);

            // Logga warning om ej hittad
            if (!isDeleted)
                _logger.LogWarning("CartItem with id {Id} was not found", id);
            else
                _logger.LogInformation("CartItem {Id} was deleted", id);

            return isDeleted;
        }


        public async Task<CartResponse?> GetCartByUserIdAsync(int userId)
        {
            _logger.LogInformation("Getting cart for user {UserId}", userId);
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            
            if(cart is null)
            {
                _logger.LogWarning("Cart for user {UserId} was not found", userId);
                return null;
            }

            var items = await _cartRepo.GetCartItemsAsync(cart.CartId);

            var cartItemResponse = items.Adapt<List<CartItemResponse>>();
            decimal totalSum = CartHelper.GetTotalSum(items);

            return new CartResponse
            {
                Items = cartItemResponse,
                Total = totalSum
            };
        }
    }
}
