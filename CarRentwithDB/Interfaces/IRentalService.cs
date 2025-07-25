using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IRentalService
    {
        Task<bool> CreateRental(Rental rental);
        //Task<List<Rental>> GetAllRentals();
        Task<IEnumerable<Rental>> GetFilteredRentals(string plate, string name, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<Rental>> GetCurrentRentals();
        Task<int> GetRentalCountAsync();
    }
}
