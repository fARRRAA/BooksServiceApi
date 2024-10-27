using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksServiceApi.Models
{
    public class Books
    {
        [Key]
        public int Id_Book { get; set; }

        public string Author { get; set; }

        [ForeignKey(nameof(Genre))]
        public int Id_Genre { get; set; }

        public Genre Genre { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public string Photo { get; set; }
    }
}
