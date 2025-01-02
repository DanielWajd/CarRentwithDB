using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage ="Adres Email jest wymagany")]
        public string EmailAddress { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Potwierdź hasło")]
        [Required(ErrorMessage = "Potwierdzone hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Hasło nie pasuje")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Imie")]
        [Required(ErrorMessage = "Imię jest wymagane")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Imie musi skłądac się tylko z liter")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nazwisko musi skąłdać się tylko z liter")]
        public string Surname { get; set; }
        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr!")]
        public string Phone { get; set; }
        public string? Image { get; set; }
        [Display(Name = "Typ użytkownika")]
        [Required(ErrorMessage = "Typ użytkownika jest wymagany")]
        public UserType UserType { get; set; }
        public RegisterCustomerViewModel? registerCustomer { get; set; }
        public RegisterEmployeeViewModel? registerEmployee { get; set; }

    }
}
