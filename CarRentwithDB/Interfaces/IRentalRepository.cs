using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IRentalRepository
    {
        Task<bool> CreateRental(Rental rental);
        //Task<List<Rental>> GetAllRentals();
        Task<IEnumerable<Rental>> GetFilteredRentals(string plate, string name, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<Rental>> GetCurrentRentals();
        Task<int> GetRentalCountAsync();
        Task<Rental> GetByIdAsync(int id);
        Task<bool> Update(Rental rental);
    }
}
