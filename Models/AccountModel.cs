using LaheKvass.Models.DB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaheKvass.Models
{
    [Table("Accounts")]
    public class AccountModel : DBModel
    {
        [Key]
        public new int Id { get; set; }

        [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*)*$", ErrorMessage = "Vale nimi")]
        [Required(ErrorMessage = "Sisesta nimi")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z][a-zA-Z]*(\s|-)?[a-zA-Z]*$", ErrorMessage = "Vale perekonnanimi")]
        [Required(ErrorMessage = "Sisesta perekonnanimi")]
        public string LastName { get; set; }

        [RegularExpression(@"^(Mees|Naine|Muud|Mitte-binaarne)$", ErrorMessage = "Vale sugu")]
        [Required(ErrorMessage = "Sisesta sugu")]
        public string Gender { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Vale email")]
        [Required(ErrorMessage = "Sisesta email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sisesta parool")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}