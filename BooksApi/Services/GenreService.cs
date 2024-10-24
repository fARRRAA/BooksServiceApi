using BooksServiceApi.Controllers;
using BooksServiceApi.dbContext;
using BooksServiceApi.Interfaces;
using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly BooksApiDb _context;



        public GenreService(BooksApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task AddNewGenre(CreateGenre createdGenre)
        {
            var check =  _context.Genre.FirstOrDefaultAsync(i => i.Name.ToLower() == createdGenre.Name.ToLower());
            var genre = new Genre() { Name = createdGenre.Name };
            await _context.Genre.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreById(int id)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            _context.Genre.Remove(genre);
            _context.SaveChanges();
        }

        public List<Genre> GetAllGenres()
        {
            return _context.Genre.ToList();
        }

        public async Task UpdateGenreById(int id, CreateGenre createdGenre)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            genre.Name = createdGenre.Name;
            await _context.SaveChangesAsync();
        }

        public bool GenreExists(int id)
        {
            return _context.Genre.Any(g => g.Id_Genre == id);

        }

    }
}
