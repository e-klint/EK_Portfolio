using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record CartItemResponse
    {
        //public BookResponse Book { get; set; } //Ta bort

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
