using BooksServiceApi.dbContext;
using BooksServiceApi.Interfaces;
using BooksServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : Controller
    {
        //public Check Check;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookService _book;
        private readonly IReaderService _reader;
        private readonly IRentService _rent;
        private readonly IBookExemplarService _exemplar;
        public RentalController(IReaderService reader, IBookService book, IRentService rentService, IBookExemplarService exemplar)
        {
            //Check = new Check(httpContextAccessor);
            _reader = reader;
            _book = book;
            _rent = rentService;
            _exemplar = exemplar;
        }
        [Authorize]
        [HttpPost("RentBookById/{bookId}")]
        public async Task<IActionResult> RentBookById(int bookId, int readerId, int rentalTime)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(readerId)) || string.IsNullOrWhiteSpace(Convert.ToString(rentalTime)))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            if (_rent.GetCurrentRentals().Any(r => r.Id_Reader == readerId && r.Id_Book == bookId))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("you already rent this book")
                });
            }
            if (!_book.BookExists(bookId))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found book with this id")
                });
            }
            if (!_reader.Exists(bookId))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }
            if (!_exemplar.ExemplarExists(bookId) || _exemplar.ExemplarCounts(bookId) == 0)
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("book with that id has 0 exemplars")
                });
            }
            await _rent.RentBookById(bookId, readerId, rentalTime);
            return Ok();
        }
        [Authorize]
        [HttpGet("getReadersRentals/{id}")]
        public async Task<IActionResult> GetReadersRentals(int id)
        {
            if (!_rent.ReaderInRent(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("reader has no rentals")
                });
            }
            return new OkObjectResult(new
            {
                rentals = _rent.GetReadersRentals(id)
            });
        }
        [Authorize]
        [HttpPost("returnRent{rentId}")]
        public async Task<IActionResult> ReturnRent(int rentId)
        {
            if (!_rent.RentExists(rentId))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found rent with this id")
                });
            }
            await _rent.ReturnRent(rentId);
            return Ok();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("getCurrentRentals")]
        public async Task<IActionResult> GetCurrentRentals()
        {
            return new OkObjectResult(new
            {
                rentals = _rent.GetCurrentRentals()
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet("getBookRentals/{id}")]
        public async Task<IActionResult> GetBookRentals(int id)
        {
            if (!_book.BookExists(id))
            {
                return new UnauthorizedObjectResult(new
                {
                    error = Unauthorized("not found book with this id")
                });
            }
            if (!_rent.BookInRentExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found rent with this book id")
                });
            }
            return new OkObjectResult(new
            {
                rentals = _rent.GetBookRentals(id)
            });
        }
    }
}
