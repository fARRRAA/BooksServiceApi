using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksServiceApi.dbContext;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Authorization;
using BooksServiceApi.Interfaces;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genre;

        public GenreController(IGenreService genre)
        {
            _genre = genre;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllGenres()
        {

            return new OkObjectResult(new
            {
                genres = _genre.GetAllGenres()
            });
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewGenre([FromQuery] CreateGenre createdGenre)
        {

            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")

                });
            }
            if (_genre.GetAllGenres().Any(g=>g.Name== createdGenre.Name))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that name already exists")
                });
            }
            await _genre.AddNewGenre(createdGenre);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, [FromQuery] CreateGenre createdGenre)
        {

            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            if (!_genre.GenreExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });
            }
            await _genre.UpdateGenreById(id, createdGenre);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            if (!_genre.GenreExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });
            }
            await _genre.DeleteGenreById(id);
            return Ok();
        }
    }
}
