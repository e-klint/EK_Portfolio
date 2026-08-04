using TheBookParlour.Core.Helpers;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepo _repo;
        private readonly ILogger<GenreService> _logger;

        public GenreService(IGenreRepo repo, ILogger<GenreService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            // Kolla om genre med samma namn redan finns?

            // Sätt CreatedAt och UrlSlug
            genre.UrlSlug = SlugHelper.GenerateSlug(genre.Name);
            genre.CreatedAt = DateTime.UtcNow;

            var addedGenre = await _repo.AddGenreAsync(genre);
            // Logga att genre skapades
            _logger.LogInformation("Genre {Name} was created", genre.Name);

            return addedGenre;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var isDeleted = await _repo.DeleteGenreAsync(id);

            // Logga warning om ej hittad
            if (!isDeleted)
                _logger.LogWarning("Genre with id {Id} was not found", id);
            else
                _logger.LogInformation("Genre {Id} was deleted", id); 
            
            return isDeleted;
        }

        public async Task<Genre?> GetGenreAsync(int id)
        {
            var genre = await _repo.GetGenreAsync(id);

            if (genre is null)
                _logger.LogWarning("Genre with id {Id} was not found", id);

            return genre;
        }

        public async Task<List<Genre>> GetGenresAsync(string? slug)
        {
            return await _repo.GetGenresAsync(slug);
        }

        public async Task<Genre?> UpdateGenreAsync(Genre genre)
        {
            // Null-check på gamla genren
            var oldGenre = await _repo.GetGenreAsync(genre.Id);

            if (oldGenre is null) {
                _logger.LogWarning("Genre with id {Id} was not found", genre.Id);
                return null;
            }

            // Regenerera UrlSlug om namnet har ändrats
            if (oldGenre.Name != genre.Name)
                genre.UrlSlug = SlugHelper.GenerateSlug(genre.Name);

            // Sätt UpdatedAt
            genre.UpdatedAt = DateTime.UtcNow;

            var updatedGenre = await _repo.UpdateGenreAsync(genre);
            // Logga att genre uppdaterades
            _logger.LogInformation("Genre {Name} was updated", genre.Name);

            return updatedGenre;
        }
    }
}
