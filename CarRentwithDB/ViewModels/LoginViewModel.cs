using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
