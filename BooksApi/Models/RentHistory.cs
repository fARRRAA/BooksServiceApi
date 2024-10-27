using BooksServiceApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksServiceApi.Models
{
    public class RentHistory
    {
        [Key]
        public int id_Rent { get; set; }
        public DateTime Rental_Start { get; set; }
        public int Rental_Time { get; set; }

        public int Id_Reader { get; set; }

        [ForeignKey(nameof(Book))]
        public int Id_Book { get; set; }
        public Books Book { get; set; }
        public DateTime Rental_End { get; set; }
        public string Rental_Status {  get; set; }


    }
}
