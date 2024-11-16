using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterCustomerViewModel
    {
        [Required(ErrorMessage = "Wpisz numer prawa jazdy.")]
        public string DrivingLicence { get; set; }
    }
}
