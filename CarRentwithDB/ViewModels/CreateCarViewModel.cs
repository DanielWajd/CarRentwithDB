using CarRentwithDB.Data.Enum;
using CarRentwithDB.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class CreateCarViewModel
    {
        [Required(ErrorMessage = "Marka pojazdu jest wymagana")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model pojazdu jest wymagany")]
        [StringLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Rok produkcji jest wymagany")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

        [Required(ErrorMessage = "Typ pojazdu jest wymagany")]
        public CarType carType { get; set; }

        [Required(ErrorMessage = "Typ paliwa jest wymagany")]
        public FuelType fuelType { get; set; }

        [Required(ErrorMessage = "Przebieg pojazdu jest wymagany")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Kolor pojazdu jest wymagany")]
        public CarColor carColor { get; set; }

        [Required(ErrorMessage = "Cena za dzień pojazdu jest wymagana")]
        public decimal DailyRate { get; set; }

        [Required(ErrorMessage = "VIN pojazdu jest wymagany")]
        [StringLength(20)]
        public string VIN { get; set; }

        [Required(ErrorMessage = "Tablica rejestracujna pojazdu jest wymagana")]
        [StringLength(10)]
        public string LicencePlate { get; set; }
        [Required(ErrorMessage = "Dostępność pojazdu jest wymagana")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Opis pojazdu jest wymagana")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Zdjęcie pojazdu jest wymagana")]
        public string Image { get; set; }
        public CarDetailsViewModel CarDetails { get; set; }
        public string AppUserId { get; set; }
        [Required(ErrorMessage = "Strona kierownicy pojazdu jest wymagana")]
        public SteeringSide steeringSide { get; set; }
    }
}
