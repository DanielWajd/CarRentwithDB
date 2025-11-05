using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IDetailsBoardRepository
    {
        Task<List<Car>> GetAllCreatedCars();
        Task<List<Rental>> GetAllUserRentals();
    }
}
