using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksServiceApi.dbContext;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using BooksServiceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BooksServiceApi.Controllers;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;
using System.Reflection.PortableExecutable;
//using LibraryWebApi.Requests;
namespace BooksServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly IBookService _books;
        private readonly IGenreService _genre;

        public BooksController(IBookService bookService,  IGenreService genreService)
        {
            _books = bookService;
            _genre = genreService;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page,
        [FromQuery] int? pageSize)
        {
            var books = _books.GetAllBooks(author, genre, year, page, pageSize);
            return new OkObjectResult(new
            {
                books = books
            });
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewBook([FromBody] CreateBook book)
        {

            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }

            if (_books.GetAll().Any(b => b.Author == book.Author && b.Title == book.Title))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("this book is already exists")
                });
            }
            await _books.AddNewBook(book);
            return Ok();
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBook book)
        {

            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });

            }
            await _books.UpdateBook(id, book);
            return Ok();

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {

            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            await _books.DeleteBook(id);
            return Ok();
        }
        [HttpGet]
        [Route("genre/{id}")]
        public async Task<IActionResult> GetBooksByGenre(int id)
        {
            if (!_genre.GenreExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that id don`t exists")
                });
            }

            return new OkObjectResult(new
            {
                genres = _books.GetBooksByGenre(id)
            });
        }
        [HttpGet]
        [Route("author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {


            if (!_books.GetAll().Any(b => b.Author == author))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found book with that author")
                });
            }
            var books = _books.GetBooksByAuthor(author);
            return new OkObjectResult(new
            {
                books = books
            });
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetBooksByName(string name, int? page, int? pageSize)
        {

            var books = _books.GetBooksByName(name,page,pageSize);
            if(books.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found book with that name")
                });
            }
            return new OkObjectResult(new
            {
                books = books
            });
        }
        [HttpGet]
        [Route("exemplars")]
        public async Task<IActionResult> GetALlExemplars()
        {

            return new OkObjectResult(new
            {
                copies = _books.GetAllExemplars()
            });
        }
        [HttpGet]
        [Route("exemplar/{bookId}")]
        public async Task<IActionResult> GetExemplar(int bookId)
        {

            if (!_books.GetAllExemplars().Any(b => b.Book_Id == bookId))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("could not find exemplars of this book")
                });
            }
            return new OkObjectResult(new
            {
                book = _books.GetExemplar(bookId)
            });
        }
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound(new { message = $"Книга с ID {id} не найдена." })
                });
            }

            return new OkObjectResult(new
            {
                book = _books.GetBookById(id)
            });

        }
        [HttpPost("uploadpfp")]
        public async Task<IActionResult> UploadProfilePhoto(int readerId, IFormFile photo)
        {
            var url = await _books.UploadProfilePhoto(readerId, photo);
            return new OkObjectResult(new { url = url });
        }
        [HttpPut("updatepfp/{readerId}")]
        public async Task<IActionResult> UpdateProfilePhoto(int readerId, IFormFile photo)
        {
            var url = await _books.UpdateProfilePhoto(readerId, photo);
            return new OkObjectResult(new { url = url });
        }

        [HttpDelete("deletepfp/{readerId}")]
        public async Task<IActionResult> DeleteProfilePhoto(int readerId)
        {
            await _books.DeleteProfilePhoto(readerId);
            return Ok();
        }
    }
}
