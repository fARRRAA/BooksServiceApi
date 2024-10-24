

namespace BooksServiceApi.Requests
{
    public class CreateBook
    {
        public string Author { get; set; }
        public int Id_Genre { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
    }
}
