using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public int Amount { get; set; } 

        [Required]
        public DateTime PaymentDate { get; set; } 


        [Required]
        [StringLength(20)]
        public string PaymentMethod { get; set; }
    }
}
