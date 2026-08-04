using System.ComponentModel.DataAnnotations;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record BookResponse
    {
        public int BookId { get; set; }

        public required string Title { get; set; }

        public required int Price { get; set; }

        public string? Plot { get; set; }

        public string? Cover { get; set; } //img url

        public string? UrlSlug { get; set; }

        public List<Author>? Authors { get; set; }  // Många-till-många

        public int GenreId { get; set; }

    }
}
