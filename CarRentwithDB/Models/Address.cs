using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Address
    {
        [Key] 
        public int AddressId { get; set; } 

        [Required] 
        [StringLength(100)] 
        public string Street { get; set; }

        [Required]
        [StringLength(10)] 
        public string HouseNumber { get; set; }

        [Required]
        [StringLength(50)] 
        public string City { get; set; }

        [Required]
        [StringLength(50)] 
        public string Voivodeship { get; set; }

        [Required]
        public string Zip { get; set; }
    }
}
