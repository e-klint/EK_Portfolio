using Microsoft.EntityFrameworkCore;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Data.Repos
{
    public class BookRepo:IBookRepo
    {   
        private readonly BookshopContext _context;

        public BookRepo(BookshopContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(string? slug, int page, int pageSize) 
        {
            var query = _context.Books.AsQueryable(); //Bygger queryn utan att hämta något än. 

            //Kontrollera om slug skickats med, filtrera i så fall.
            if (!string.IsNullOrEmpty(slug))
                query = query.Where(b => b.UrlSlug == slug);
             
             // 10 böcker per sida       
                    query = query
              .Skip((page - 1) * pageSize)
              .Take(pageSize);

            return await query.ToListAsync();
        }
        public async Task<Book?> GetBookAsync(int id)
        {
            return await _context.Books
                .AsNoTracking() // ← hämta utan tracking (annars kan EF returnera samma objekt från sin interna cache, blir problem när man uppdaterar slug)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync(); 
            return book;
        }
        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);

            if (bookToDelete is null)
                return false;

            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
