using CarRentwithDB.Data.Enum;
using CarRentwithDB.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public UserType UserType { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
