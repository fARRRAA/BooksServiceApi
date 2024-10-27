using BooksServiceApi.dbContext;
using BooksServiceApi.Interfaces;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksServiceApi.Services
{
    public class BookService : IBookService
    {
        readonly BooksApiDb _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BookService(BooksApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public List<Books> GetAllBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            IQueryable<Books> query = _context.Books.Include(b => b.Genre);

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author == author);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Name == genre);
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.Year.Year == year.Value);
            }

            var totalBooks = query.Count();
            var books = new List<Books>();

            if (page.HasValue && pageSize.HasValue)
            {
                books = query.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return books;
            }

           return query.ToList();

        }
        public List<Books> GetBooksByName(string name, int? page, int? pageSize)
        {
            var nameParts = name.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var books = _context.Books.Where(i => nameParts.Any(part => i.Title.ToLower().Contains(part))).Include(b => b.Genre).ToList();
            if (page.HasValue && pageSize.HasValue)
            {
                books = books.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return books;
            }
            return books;
        }
        public async Task AddNewBook(CreateBook book)
        {
            var Book = new Books()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Year = book.Year,
                Id_Genre = book.Id_Genre
            };
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBook(int id, CreateBook book)
        {
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            temp.Title = book.Title;
            temp.Author = book.Author;
            temp.Description = book.Description;
            temp.Year = book.Year;
            temp.Id_Genre = book.Id_Genre;

            await _context.SaveChangesAsync();

        }
        public async Task DeleteBook(int id)
        {
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            _context.Books.Remove(temp);
            _context.SaveChanges();
        }

        public List<Books> GetBooksByAuthor(string author)
        {
            var nameParts = author.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books =  _context.Books.Where(i => nameParts.All(part => i.Author.ToLower().Contains(part))).ToList();
            return books;

        }
        public List<BookExemplar> GetAllExemplars()
        {
            var allCopies =  _context.BookExemplar.ToList();
            return allCopies;
        }

        public List<Books> GetBooksByGenre(int id)
        {   
            return _context.Books.Where(b=>b.Id_Genre == id).ToList();
        }



        public Books GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id_Book == id);
        }

        public BookExemplar GetExemplar(int bookId)
        {
            var exemplarBook = _context.BookExemplar.Include(e => e.Book).FirstOrDefault(e => e.Book_Id == bookId); 
            return exemplarBook;
        }

        public bool BookExists(int id)
        {
            return (_context.Books.Any(b => b.Id_Book == id));

        }
        public List<Books> GetAll()
        {
            return _context.Books.ToList();
        }


    }
}
