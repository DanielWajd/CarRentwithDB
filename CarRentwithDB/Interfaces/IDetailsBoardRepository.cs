using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IDetailsBoardRepository
    {
        Task<List<Car>> GetAllCreatedCars(string userId);
        Task<List<Rental>> GetAllUserRentals(string userId);
    }
}
