using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IRentalService
    {
        Task<bool> CreateRental(Rental rental);
    }
}
