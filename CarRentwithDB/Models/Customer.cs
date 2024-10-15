using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string DrivingLicence { get; set; }
    }
}
