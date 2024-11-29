using CarRentwithDB.Models;

namespace CarRentwithDB.ViewModels
{
    public class DetailsBoardViewModel
    {
        public List<Car> Cars { get; set; }
        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; }
        public List<Rental> Rentals { get; set; }
    }
}
