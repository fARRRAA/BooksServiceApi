using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksServiceApi.Models
{
    public class Genre
    {
        [Key]
        public int Id_Genre { get; set; }
        public string Name { get; set; }
    }
}
