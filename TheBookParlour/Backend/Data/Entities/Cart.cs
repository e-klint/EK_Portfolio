using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<CartItem> Items { get; set; } // ← navigation property
    }
}
