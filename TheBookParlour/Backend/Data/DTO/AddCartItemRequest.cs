using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record AddCartItemRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
