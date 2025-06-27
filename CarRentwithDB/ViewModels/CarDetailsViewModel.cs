using CarRentwithDB.Data.Enum;

namespace CarRentwithDB.ViewModels
{
    public class CarDetailsViewModel
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public CarType carType { get; set; }
        public int Mileage { get; set; }
        public CarColor carColor { get; set; }
        public FuelType fuelType { get; set; }
        public TechCarDetailsViewModel TechCarDetails { get; set; }
        public SteeringSide steeringSide { get; set; }
        public decimal DailyRate { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; }
    }
}
