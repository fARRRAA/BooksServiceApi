using BooksServiceApi.Models;
using BooksServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BooksServiceApi.Interfaces
{
    public interface IGenreService
    {
        public List<Genre> GetAllGenres();
        public Task AddNewGenre(CreateGenre createdGenre);
        public Task UpdateGenreById(int id, CreateGenre createdGenre);
        public Task DeleteGenreById(int id);
        public bool GenreExists(int id);
    }
}
