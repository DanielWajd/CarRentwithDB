using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Image { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public RegisterCustomerViewModel? registerCustomer { get; set; }
        public RegisterEmployeeViewModel? registerEmployee { get; set; }

    }
}
