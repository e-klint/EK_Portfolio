using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.Interfaces
{
    public interface IBookRepo
    {

        Task<List<Book>> GetBooksAsync(string? slug, int page, int pageSize);
        Task<Book?> GetBookAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);

    }
}
