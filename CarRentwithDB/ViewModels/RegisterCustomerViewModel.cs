using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterCustomerViewModel
    {

        [Required(ErrorMessage = "Wpisz numer prawa jazdy.")]
        [MaxLength(11, ErrorMessage ="Numer prawa jazdy nie może być dłuższy niż 11 znaków.")]
        public string DrivingLicence { get; set; }

        [Required(ErrorMessage = "Wpisz nazwę ulicy.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Wpisz numer domu.")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Wpisz nazwę miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Wpisz województwo.")]
        public string Voivodeship { get; set; }

        [Required(ErrorMessage = "Wpisz kod pocztowy.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage ="Kod pocztowy musi mieć format XX-XXX.")]
        public string Zip { get; set; }
    }
}
