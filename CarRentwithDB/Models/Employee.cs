using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
