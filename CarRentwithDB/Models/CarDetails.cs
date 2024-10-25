using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRentwithDB.Data.Enum;

namespace CarRentwithDB.Models
{
    public class CarDetails
    {
        [Key]
        public int CarDetailsId { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
        [Required]
        public int HorsePower { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public decimal EngineCapacity { get; set; }

        [Required]
        public decimal TrunkCapacity { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }
    }
}
