using BooksServiceApi.Models;

namespace BooksServiceApi.Interfaces
{
    public interface IReaderService
    {
        public Readers GetById(int id);
        public bool Exists(int id);
    }
}
