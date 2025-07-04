using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterEmployeeViewModel
    {
        [Required(ErrorMessage = "Wybierz typ pracownika.")]
        public EmployeeType EmployeeType { get; set; }
    }
}
