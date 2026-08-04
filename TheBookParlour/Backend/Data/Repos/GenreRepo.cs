using Microsoft.EntityFrameworkCore;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Data.Repos
{
    public class GenreRepo : IGenreRepo
    {
        private readonly BookshopContext _context;

        public GenreRepo(BookshopContext context)
        {
            _context = context;
        }
        

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genreToDelete = await GetGenreAsync(id);

            if (genreToDelete is null)
                return false;

           _context.Genres.Remove(genreToDelete);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Genre> GetGenreAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books) 
                .FirstOrDefaultAsync(g => g.Id == id);   
        }

        public async Task<List<Genre>> GetGenresAsync(string? slug)
        {
            //Bygger queryn utan att hämta något än
            var query = _context.Genres
                .Include(genre => genre.Books)
                .AsQueryable();

            //Kontrollera om slug skickats med, filtrera i så fall.
            if (!string.IsNullOrWhiteSpace(slug))
                query = query.Where(g => g.UrlSlug == slug);

            return await query.ToListAsync();
        }

        public async Task<Genre> UpdateGenreAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre; 
        }
    }
}
