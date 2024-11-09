using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IDetailsBoardService
    {
        Task<List<Car>> GetAllCreatedCars();
        Task<List<Car>> GetAllRentedCars();
    }
}
