using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetGenresAsync(string? slug);
        Task<Genre?> GetGenreAsync(int id);
        Task<Genre> AddGenreAsync(Genre genre);
        Task<Genre?> UpdateGenreAsync(Genre genre);
        Task<bool> DeleteGenreAsync(int id);
    }
}
