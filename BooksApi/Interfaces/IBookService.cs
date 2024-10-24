using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BooksServiceApi.Interfaces
{
    public interface IBookService
    {
        public List<Books> GetAllBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page, [FromQuery] int? pageSize);
        public Task AddNewBook(CreateBook book);
        public Task UpdateBook(int id, CreateBook book);
        public Task DeleteBook(int id);
        public List<Books> GetBooksByGenre(int id);
        public List<Books> GetBooksByAuthor(string author);
        public List<Books> GetBooksByName(string name, int? page, int? pageSize);
        public List<BookExemplar> GetAllExemplars();
        public BookExemplar GetExemplar(int bookId);
        public Books GetBookById(int id);

        public bool BookExists(int id);
        public List<Books> GetAll();
    }
}
 