using CarRentwithDB.Data.Enum;
using CarRentwithDB.Models;

namespace CarRentwithDB.ViewModels
{
    public class EditCarViewModel
    {
        public int CarId { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }

        public int Year { get; set; }
        public string City { get; set; }

        public CarType carType { get; set; }

        public FuelType fuelType { get; set; }

        public int Mileage { get; set; }

        public CarColor carColor { get; set; }

        public decimal DailyRate { get; set; }

        public string VIN { get; set; }
        public SteeringSide steeringSide { get; set; }

        public string LicencePlate { get; set; }

        public bool IsAvailable { get; set; }

        public string Description { get; set; }

        public IFormFile? ImageFile { get; set; }
        public byte[]? Image {  get; set; }
        public TechCarDetailsViewModel CarDetails { get; set; }

    }
}
