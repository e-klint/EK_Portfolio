using Microsoft.AspNetCore.Mvc;
using TheBookParlour.Core.Helpers;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.Entities;
using TheBookParlour.Data.Interfaces;

namespace TheBookParlour.Core.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepo bookRepo, ILogger<BookService> logger) 
        {
            _bookRepo = bookRepo;
            _logger = logger;
        }

        public async Task<List<Book>> GetBooksAsync(string? slug, int page, int pageSize)
        {
            return await _bookRepo.GetBooksAsync(slug, page, pageSize);  
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            var book = await _bookRepo.GetBookAsync(id);

            if (book is null)
                _logger.LogWarning("Book with id {Id} was not found", id);

            return book;

        }

        public async Task<Book> AddBookAsync(Book book)
        {
            // Kolla om bok med samma namn redan finns?

            //Set UrlSlug and CreatedAt
            book.UrlSlug = SlugHelper.GenerateSlug(book.Title);
            book.CreatedAt = DateTime.UtcNow;

            var addedBook = await _bookRepo.AddBookAsync(book);
            _logger.LogInformation("Book {Title} was created", book.Title);

            return addedBook; 
        }

        public async Task<Book?> UpdateBookAsync(Book book)
        {
            // Regenerera UrlSlug om titeln har ändrats
            var oldBook = await _bookRepo.GetBookAsync(book.BookId);

            if (oldBook is null) {
            _logger.LogWarning("Book with id {Id} was not found", book.BookId);
            return null;
            }

            // Regenerera UrlSlug om titeln har ändrats
            if (oldBook.Title != book.Title)
                book.UrlSlug = SlugHelper.GenerateSlug(book.Title);

            // Sätt UpdatedAt
            book.UpdatedAt = DateTime.UtcNow;

            var updatedBook = await _bookRepo.UpdateBookAsync(book);
            _logger.LogInformation("Book {Id} was updated", book.BookId);

            return updatedBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var isDeleted = await _bookRepo.DeleteBookAsync(id);

            if (!isDeleted)
                _logger.LogWarning("Book with id {Id} was not found", id);
            else
                _logger.LogInformation("Book {Id} was deleted", id);

            return isDeleted;
        }
    }
}
