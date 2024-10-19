using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CarRentwithDB.Models
{
    public class Customer : AppUser
    {
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [StringLength(20)]
        public string DrivingLicence { get; set; }
    }
}
