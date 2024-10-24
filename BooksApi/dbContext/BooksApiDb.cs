using Microsoft.EntityFrameworkCore;
using BooksServiceApi.Models;
namespace BooksServiceApi.dbContext
{
    public class BooksApiDb:DbContext
    {
        public BooksApiDb(DbContextOptions options) :base(options) { }

        public DbSet<Books> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BookExemplar> BookExemplar { get; set;}

    }
}
