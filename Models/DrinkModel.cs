
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaheKvass.Models
{
    [Table("Drinks")]
    public class DrinkModel
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*)*$", ErrorMessage = "Vale joogi nimi")]
        [Required(ErrorMessage = "Sisesta joogi nimi")]
        public string DrinkName { get; set; }
        [RegularExpression(@"^(Kvass|Õlu)$", ErrorMessage = "Vale tüüp")]
        [Required(ErrorMessage = "Sisesta tüüp")]
        public string Type { get; set; }
        [RegularExpression(@"[A-Za-z0-9,.%!?\-'\s]+", ErrorMessage = "Vale kirjeldus")]
        [Required(ErrorMessage = "Sisesta kirjeldus")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Sisesta hind")]
        public double Price { get; set; }
    }
}