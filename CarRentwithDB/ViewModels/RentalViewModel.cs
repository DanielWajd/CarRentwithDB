using System.ComponentModel.DataAnnotations;

namespace CarRentwithDB.ViewModels
{
    public class RentalViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.Today;

        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
        public int CarId { get; set; }
        public string AppUserId { get; set; }
        public decimal Price { get; set; }
        public decimal DailyRate { get; set; }
    }
}
