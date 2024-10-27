using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
namespace BooksServiceApi.Models
{
    public class Readers
    {
        [Key]
        public int Id_User { get; set; }
        public string Name { get; set; }
        public DateTime Date_Birth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey(nameof(Role))]
        public int? Id_Role { get; set; } = 2;
        public Roles Role { get; set; }

    }
}
