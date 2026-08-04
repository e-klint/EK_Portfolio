using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Image { get; set; } //Använd för ikon

        public string? UrlSlug { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Book>? Books { get; set; } //Navigation property
    }
}
