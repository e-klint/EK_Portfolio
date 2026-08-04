using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } // ← navigation property

        public int BookId { get; set; }

        public Book Book { get; set; }    // ← navigation property

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
