using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterCustomerViewModel
    {
        [Display(Name = "Numer prawa jazdy")]
        [Required(ErrorMessage = "Wpisz numer prawa jazdy.")]
        public string DrivingLicence { get; set; }
    }
}
