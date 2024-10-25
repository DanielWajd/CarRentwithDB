using CarRentwithDB.Data.Enum;
using CarRentwithDB.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class CreateCarViewModel
    {
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public CarType carType { get; set; }

        [Required]
        public FuelType fuelType { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public CarColor carColor { get; set; }

        [Required]
        public decimal DailyRate { get; set; }

        [Required]
        [StringLength(20)]
        public string VIN { get; set; }

        [Required]
        [StringLength(10)]
        public string LicencePlate { get; set; }

        public bool IsAvailable { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public CarDetailsViewModel CarDetails { get; set; }
    }
}
