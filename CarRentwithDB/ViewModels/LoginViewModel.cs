using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class LoginViewModel
    {
        //[Display(Name = "Email Address")]
        //[Required(ErrorMessage = "Adres email jest wymagany")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
