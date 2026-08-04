using System.ComponentModel.DataAnnotations;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record AddBookRequest
    {
        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)] //Förhindrar att tom stäng skickas in.
        public required string Title { get; set; }

        [Required]
        public required int Price { get; set; }

        [MaxLength(1000)]
        public string? Plot { get; set; }

        public string? Cover { get; set; }

        public int? GenreId { get; set; }
        //public List<int>? AuthorIds { get; set; }  // ← bara Id:n, inte Author-objekt
    }
}
