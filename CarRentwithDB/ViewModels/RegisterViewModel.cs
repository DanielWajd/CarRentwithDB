using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Adres Email jest wymagany")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Potwierdzone hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Hasło nie pasuje")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$", ErrorMessage = "Imię może zawierać tylko litery")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$", ErrorMessage = "Nazwisko może zawierać tylko litery")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr!")]
        public string Phone { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Typ użytkownika jest wymagany")]
        public UserType UserType { get; set; }
        public RegisterCustomerViewModel? registerCustomer { get; set; }
        public RegisterEmployeeViewModel? registerEmployee { get; set; }

    }
}
