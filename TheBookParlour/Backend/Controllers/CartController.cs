using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet] //Scalar- Ok!
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCart()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                _logger.LogWarning("Invalid user id in token");
                return Unauthorized();
            }

            _logger.LogInformation("GET /api/cart requested for {UserId}", userId);
            var cartResponse = await _cartService.GetCartByUserIdAsync(userId);

            if (cartResponse is null)
            {
                _logger.LogWarning("Cart for user {UserId} was not found", userId);
                return NotFound();
            }

            return Ok(cartResponse);
        }

        
        [HttpPost] //Scalar - OK!
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddItemToCart(AddCartItemRequest request)
        {
                if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddCartItem - invalid request");
                return BadRequest(ModelState);
            }
            
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                _logger.LogWarning("Invalid user id in token");
                return Unauthorized();
            }

            var addedItem = await _cartService.AddCartItemAsync(userId, request); //(added item mappas till dto i servicen)

            if (addedItem is null)
            {
                _logger.LogWarning("Could not add item to cart for user {UserId}", userId);
                return BadRequest("Could not add item to cart.");
            }

            return CreatedAtAction(nameof(GetCart), addedItem); 
        }

        [HttpDelete("{id}")] //Scalar- OK!
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                _logger.LogWarning("Invalid user id in token");
                return Unauthorized();
            }

            bool isDeleted = await _cartService.DeleteCartItemAsync(id);

            if (!isDeleted)
            {
                _logger.LogWarning("Could not remove item {Id} from cart for user {UserId}", id, userId);
                return NotFound("Could not find item in cart.");
            }

            return NoContent();
        }
    }
}
