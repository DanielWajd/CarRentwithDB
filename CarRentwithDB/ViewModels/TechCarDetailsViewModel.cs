using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class TechCarDetailsViewModel
    {
        [Required(ErrorMessage = "Liczba koni pojazdu jest wymagana")]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = "Ilość miejsc pojazdu jest wymagana")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Pojemność silnika pojazdu jest wymagana")]
        public decimal EngineCapacity { get; set; }

        [Required(ErrorMessage = "Pojemnośc bagażnika pojazdu jest wymagana")]
        public decimal TrunkCapacity { get; set; }

        [Required(ErrorMessage = "Rodzaj skrzyni biegów pojazdu jest wymagany")]
        public TransmissionType TransmissionType { get; set; }
    }
}
