using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterCustomerViewModel
    {

        [Display(Name = "Numer prawa jazdy")]
        [Required(ErrorMessage = "Wpisz numer prawa jazdy.")]
        public string DrivingLicence { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Wpisz nazwę ulicy.")]
        public string Street { get; set; }

        [Display(Name = "Numer domu")]
        [Required(ErrorMessage = "Wpisz numer domu.")]
        public string HouseNumber { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Wpisz nazwę miasta.")]
        public string City { get; set; }

        [Display(Name = "Województwo")]
        [Required(ErrorMessage = "Wpisz województwo.")]
        public string Voivodeship { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Wpisz kod pocztowy.")]
        public string Zip { get; set; }
    }
}
