using CarRentwithDB.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class AppUser : IdentityUser
    {

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }


        [StringLength(20)]
        public string Phone { get; set; }

        public UserType UserType { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public string? Image { get; set; }
    }
}
