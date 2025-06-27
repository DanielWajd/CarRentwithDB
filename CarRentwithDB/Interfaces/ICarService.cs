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
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> Delete(Car car);
        Task<bool> Save();
        Task UpdateCarAvailability(int carId, bool isAvailable);
        Task<IEnumerable<Car>> GetFilteredCars(string city, string type, string makeModel, string sortOrder);
        Task<List<Car>> GetUnAvailableCarsAsyncToUpdate();
        Task<List<Car>> GetUnAvailableCarsAsync();
        Task<List<string>> GetCarsCities();
        //Task<List<Car>> GetSortedCars(string sort);
    }
}
