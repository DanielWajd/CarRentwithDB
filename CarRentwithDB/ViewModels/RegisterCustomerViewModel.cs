using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterCustomerViewModel
    {

        [Required(ErrorMessage = "Wpisz numer prawa jazdy.")]
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
        public string Zip { get; set; }
    }
}
