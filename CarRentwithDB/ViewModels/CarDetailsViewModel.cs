using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class CarDetailsViewModel
    {
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
