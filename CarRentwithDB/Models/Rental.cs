using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; }
    }
}
