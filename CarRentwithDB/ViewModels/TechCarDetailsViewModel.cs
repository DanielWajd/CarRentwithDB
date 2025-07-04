using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class TechCarDetailsViewModel
    {
        [Required(ErrorMessage = "Liczba koni pojazdu jest wymagana")]
        [Range(0, 999, ErrorMessage = "Liczba koni pojazdu musi być z zakresu od 0 do 999KM")]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = "Ilość miejsc pojazdu jest wymagana")]
        [Range(0, 10, ErrorMessage = "Liczba miejsc pojazdu musi być z zakresu od 0 do 10")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Pojemność silnika pojazdu jest wymagana")]
        [Range(0, 10, ErrorMessage = "Pojemność silnika pojazdu musi być z zakresu od 0 do 10L")]
        public decimal EngineCapacity { get; set; }

        [Required(ErrorMessage = "Pojemnośc bagażnika pojazdu jest wymagana")]
        [Range(0, 999, ErrorMessage = "Pojemność bagażnika pojazdu musi być z zakresu od 0 do 999L")]
        public decimal TrunkCapacity { get; set; }

        [Required(ErrorMessage = "Rodzaj skrzyni biegów pojazdu jest wymagany")]
        public TransmissionType TransmissionType { get; set; }
    }
}
