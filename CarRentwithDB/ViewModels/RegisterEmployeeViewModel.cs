using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterEmployeeViewModel
    {
        [Display(Name = "Typ pracownika")]
        [Required(ErrorMessage = "Wybierz typ pracownika.")]
        public EmployeeType EmployeeType { get; set; }
    }
}
