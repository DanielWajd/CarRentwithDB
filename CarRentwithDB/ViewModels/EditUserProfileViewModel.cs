using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }


        public string? EmailAddress { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        public string? Image { get; set; }
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }

    }
}
