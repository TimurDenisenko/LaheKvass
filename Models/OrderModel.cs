
using LaheKvass.Models.DB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaheKvass.Models
{
    [Table("Orders")]
    public class OrderModel : DBModel
    {
        [Key]
        public new int Id { get; set; }
        [Required(ErrorMessage = "Sisesta kasutaja id")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Sisesta joogi id")]
        public int DrinkId { get; set; }
    }
}