using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RentalViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
