namespace BooksServiceApi.Interfaces
{
    public interface IBookExemplarService
    {
        public bool ExemplarExists(int id);
        public int ExemplarCounts(int id);
    }
}
