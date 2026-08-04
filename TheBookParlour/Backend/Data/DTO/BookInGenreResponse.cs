using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{   
    public record BookInGenreResponse
    {
        //Används i GenreWithBooksResponse
        public int BookId { get; set; }
        public string Title { get; set; }

        public int Price { get; set; }

        public string? Plot { get; set; }

        public string? Cover { get; set; } //Motsvarar Image

        public string? UrlSlug { get; set; }

    }
}

