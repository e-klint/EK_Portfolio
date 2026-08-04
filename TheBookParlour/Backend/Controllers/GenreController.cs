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
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpGet] //Scalar - OK!
        public async Task<IActionResult> GetGenres(string? slug)
        {
            _logger.LogInformation("GET /api/genres requested, slug: {Slug}", slug);
            var genres = await _genreService.GetGenresAsync(slug);

            //Mappa till DTO med mapster
            var genreResponse = genres.Adapt<List<GenreWithBooksResponse>>();

            return Ok(genreResponse);
        }

        [HttpGet("{id}")] //Scalar- OK!
        public async Task<IActionResult> GetGenrebyId(int id)
        {
            _logger.LogInformation("GET /api/genres/{Id} requested", id);
            var genre = await _genreService.GetGenreAsync(id);

            

            if (genre is null)
            {
                _logger.LogWarning("GET /api/genres/{Id} - not found", id);
                return NotFound();
            }
            
            //Mappa till DTO med mapster
            var genreResponse = genre.Adapt<GenreWithBooksResponse>();

            return Ok(genreResponse);
        }

        [HttpPost] //Scalar-OK!
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenre(AddGenreRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddGenre - invalid request");
                return BadRequest(ModelState);
            }  

            //Mappa till Entity med mapster
            var genre = request.Adapt<Genre>();

            var addedGenre = await _genreService.AddGenreAsync(genre);

            if (addedGenre is null)
            {
                _logger.LogWarning("AddGenre - could not add genre.");
                return BadRequest("Could not add genre");
            }

            //Mappa till DTO
            var genreResponse = addedGenre.Adapt<GenreResponse>();

            return CreatedAtAction(nameof(GetGenrebyId), new { id = genreResponse.Id }, genreResponse);
        }

        [HttpPatch("{id}")] //Scalar-OK!
        [Authorize(Roles = "Admin")]
        [Consumes("application/json-patch+json")] //För att Scalar ska förstå vilken ContentType som ska användas. (I Postman behöver man lägga till Content-Type: application/json-patch+json under Headings).
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] JsonPatchDocument<Genre> patchDoc) //Hämtar automatiskt {id} i routen.(patchDoc är requesten)
        {
            _logger.LogInformation("PATCH /api/genres/{Id} requested", id);

            var genre = await _genreService.GetGenreAsync(id);

            if (genre is null)
            {
                _logger.LogWarning("UpdateGenre - genre with id {Id} not found", id);
                return NotFound();
            }

            //Applicera instruktionerna på book-objektet
            patchDoc.ApplyTo(genre, jsonPatchError =>
            {
                //Om något går fel, lägg till felmeddelande i ModelState
                ModelState.AddModelError(jsonPatchError.AffectedObject.ToString(), jsonPatchError.ErrorMessage);
            });

            if (string.IsNullOrWhiteSpace(genre.Name))
                ModelState.AddModelError("Name", "Name cannot be empty or whitespace.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("UpdateGenre - invalid request");
                return BadRequest(ModelState);
            }
                
            var updatedGenre = await _genreService.UpdateGenreAsync(genre);

            if (updatedGenre is null)
            {
                _logger.LogWarning("UpdateGenre - could not update genre.");
                return BadRequest("Could not update genre");
            }
                
            //Mappa till DTO med mapster
            var genreResponse = updatedGenre.Adapt<GenreResponse>(); 

            return Ok(genreResponse);
        }

        [HttpDelete("{id}")] //Scalar-OK!
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            _logger.LogInformation("DELETE /api/genres/{Id} requested", id);
            bool isDeleted = await _genreService.DeleteGenreAsync(id);

            if (!isDeleted)
            {
                _logger.LogWarning("DELETE /api/genres/{Id} - not found", id);
                return NotFound();
            }  

            return NoContent();
        }
    }
}
