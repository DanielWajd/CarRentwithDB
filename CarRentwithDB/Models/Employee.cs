using CarRentwithDB.Data.Enum;

namespace CarRentwithDB.Models
{
    public class Employee : AppUser
    {
        public EmployeeType EmployeeType { get; set; }
    }
}
