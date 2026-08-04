using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class Book
    {
        [Key] 
        public int BookId { get; set; } 

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [Required]
        public required int Price { get; set; }

        [MaxLength(1000)]
        public string? Plot { get; set; }

        public string? Cover { get; set; }

        public string? UrlSlug { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Author>? Authors { get; set; }  // Många-till-många
        public int? GenreId { get; set; }      // ← foreign key
        public Genre? Genre { get; set; }      // ← navigation property


    }
}
