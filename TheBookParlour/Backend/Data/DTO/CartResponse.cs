using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record CartResponse
    {
        public List<CartItemResponse> Items { get; set; } 

        public decimal Total { get; set; }
    }
}
