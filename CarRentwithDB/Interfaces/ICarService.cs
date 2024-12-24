using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;

namespace CarRentwithDB.Interfaces
{
    public interface ICarService
    {

        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetByIdAsync(int id);
        Task<Car> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Car>> GetCarByCity(string city);
        Task<IEnumerable<Car>> GetCarByType(string type);
        bool Add(Car car);
        bool Update(Car car);
        bool Delete(Car car);
        bool Save();
        Task UpdateCarAvailability(int carId, bool isAvailable);
        Task<IEnumerable<Car>> GetFilteredCars(string city, string type, string makeModel);
        Task<List<Car>> GetUnAvailableCarsAsync();
        Task<List<Car>> GetSortedCars(string sort);
    }
}
