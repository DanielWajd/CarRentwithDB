using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; } 

        [Required]
        [StringLength(50)]
        public string Make { get; set; } 

        [Required]
        [StringLength(50)]
        public string Model { get; set; } 

        [Required]
        public int Year { get; set; } 

        [Required]
        public CarType carType { get; set; }

        [Required]
        public FuelType fuelType { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        [StringLength(30)]
        public CarColor carColor { get; set; }

        [Required]
        public decimal DailyRate { get; set; }

        [Required]
        [StringLength(20)]
        public string VIN { get; set; } 

        [Required]
        [StringLength(10)]
        public string LicencePlate { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public string Image { get; set; }
    }
}
