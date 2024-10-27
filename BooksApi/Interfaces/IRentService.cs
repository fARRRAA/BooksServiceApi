using BooksServiceApi.Models;

namespace BooksServiceApi.Interfaces
{
    public interface IRentService
    {
        public Task RentBookById(int id, int readerId, int rentalTime);
        public List<RentHistory> GetReadersRentals(int id);
        public Task ReturnRent(int rentId);
        public List<RentHistory> GetCurrentRentals();

        public List<RentHistory> GetBookRentals(int id);
        public bool BookInRentExists(int bookId);
        public bool RentExists(int id);
        public bool ReaderInRent(int id);
    }
}
