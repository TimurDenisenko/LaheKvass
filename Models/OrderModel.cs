
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaheKvass.Models
{
    [Table("Orders")]
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Sisesta kasutaja id")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Sisesta joogi id")]
        public int DrinkId { get; set; }
    }
}