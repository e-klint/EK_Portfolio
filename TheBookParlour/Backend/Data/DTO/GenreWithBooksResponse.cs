using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.DTO
{
    public record GenreWithBooksResponse
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string Image { get; set; }

        public List<BookInGenreResponse> Books { get; set; }
    }
}
