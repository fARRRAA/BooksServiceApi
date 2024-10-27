
using BooksServiceApi.dbContext;
using BooksServiceApi.Interfaces;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksServiceApi.Services
{
    public class BookExemplarService(BooksApiDb context) : IBookExemplarService
    {
       private readonly BooksApiDb _context = context;
        public bool ExemplarExists(int id)
        {
            return _context.BookExemplar.Any(e => e.Book_Id == id);
        }
        public int ExemplarCounts(int id)
        {
            var exemplar = _context.BookExemplar.FirstOrDefault(e => e.Book_Id == id);
            return exemplar.Books_Count;
        }
    }
}
