using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IDetailsBoardService
    {
        Task<List<Car>> GetAllCreatedCars();
        Task<List<Rental>> GetAllUserRentals();
    }
}
