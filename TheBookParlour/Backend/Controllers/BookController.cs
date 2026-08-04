using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Core.Services;
using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }


       
        [HttpGet] //Scalar- OK!
        public async Task<IActionResult> GetBooks(string? slug, int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("GET /api/books requested, slug: {Slug}, page: {Page}", slug, page);
            var books = await _bookService.GetBooksAsync(slug, page, pageSize);

           // Mappa till DTO med mapster
            var bookResponse = books.Adapt<List<BookResponse>>();

            return Ok(bookResponse);
        }

        [HttpGet("{id}")] //Scalar -OK!
        public async Task<IActionResult> GetBookbyId(int id)
        {
            _logger.LogInformation("GET /api/books/{Id} requested", id);
            var book=  await _bookService.GetBookAsync(id);

            if (book is null)
            {
                _logger.LogWarning("GET /api/books/{Id} - not found", id);
                return NotFound();
            }

            //Mappa till DTO med mapster
            var bookResponse = book.Adapt<BookResponse>();

            return Ok(bookResponse); 
        }

        [HttpPost] // Scalar- OK
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook(AddBookRequest request)
        {
            //ModelState
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddBook - invalid request");
                return BadRequest(ModelState);
            }
                
            //Mappa request till book
            var book= request.Adapt<Book>();

            //Anropa serviece add book
            var addedBook = await _bookService.AddBookAsync(book);

            //if added book is null, return "could not add book"
            if (addedBook is null)
            {
                _logger.LogWarning("AddBook - could not add book.");
                return BadRequest("Could not add book.");
            }

            //Mappa till DTO
            var bookResponse = addedBook.Adapt<BookResponse>();

            return CreatedAtAction(nameof(GetBookbyId), new {id = bookResponse.BookId}, bookResponse);
        }

        [HttpPatch("{id}")] //Scalar- OK!
        [Authorize(Roles ="Admin")]
        [Consumes("application/json-patch+json")] //För att Scalar ska förstå vilken ContentType som ska användas. (I Postman behöver man lägga till Content-Type: application/json-patch+json under Headings).
        public async Task<IActionResult> UpdateBook(int id, [FromBody] JsonPatchDocument<Book> patchDoc) //Hämtar automatiskt {id} i routen.(patchDoc är requesten)
        {
            _logger.LogInformation("PATCH /api/books/{Id} requested", id);

            var book = await _bookService.GetBookAsync(id);

            if (book is null)
            {
                _logger.LogWarning("UpdateBook - book with id {Id} not found", id);
                return NotFound();
            }
                

            //Applicera instruktionerna på book-objektet
            patchDoc.ApplyTo(book, jsonPatchError =>
            {
                //Om något går fel, lägg till felmeddelande i ModelState
                ModelState.AddModelError(jsonPatchError.AffectedObject.ToString(), jsonPatchError.ErrorMessage);
            });

            if (string.IsNullOrWhiteSpace(book.Title))
                ModelState.AddModelError("Title", "Title cannot be empty or whitespace.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("UpdateBook - invalid request");
                return BadRequest(ModelState);
            }

            var updatedBook = await _bookService.UpdateBookAsync(book);

            if (updatedBook is null)
            {
                _logger.LogWarning("UpdateBook - could not update book.");
                return BadRequest("Could not update book");
            }

            //Mappa till DTO med mapster
            var bookResponse = updatedBook.Adapt<BookResponse>();

            return Ok(bookResponse); 
        }

        [HttpDelete("{id}")] //Scalar- OK!
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("DELETE /api/books/{Id} requested", id);
            bool isDeleted = await _bookService.DeleteBookAsync(id);

            if (!isDeleted)
            {
                _logger.LogWarning("DELETE /api/books/{Id} - not found", id);
                return NotFound();
            }
     
            return NoContent();
        }
    }
}
