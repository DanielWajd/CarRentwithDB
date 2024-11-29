using CarRentwithDB.Data.Enum;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentDBContext _context;

        public CarService(CarRentDBContext context)
        {
            _context = context;
        }
        public bool Add(Car car)
        {
            _context.Add(car);
            return Save();
        }

        public bool Delete(Car car)
        {
            _context.Remove(car);
            return Save();
        }


        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.Include(i => i.CarDetails).FirstOrDefaultAsync(i => i.CarId == id);
        }
        public async Task<Car> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Cars.Include(i => i.CarDetails).AsNoTracking().FirstOrDefaultAsync(i => i.CarId == id);
        }
        public async Task<IEnumerable<Car>> GetCarByCity(string city)
        {
            return await _context.Cars.Where(i => i.City.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarByType(string type)
        {

            if (Enum.TryParse(type, true, out CarType carTypeEnum))
            {
                return await _context.Cars.Where(i => i.carType == carTypeEnum).ToListAsync();
            }

            return Enumerable.Empty<Car>();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Car car)
        {
            _context.Update(car);
            return Save();
        }
        public async Task UpdateCarAvailability(int carId, bool isAvailable)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                car.IsAvailable = isAvailable;
                await _context.SaveChangesAsync();
            }

        }
        public async Task<IEnumerable<Car>> GetFilteredCars(string city, string type)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(car => car.City.Contains(city));
            }

            if (!string.IsNullOrEmpty(type) && Enum.TryParse(type, true, out CarType carTypeEnum))
            {
                query = query.Where(car => car.carType == carTypeEnum);
            }
            return await query.ToListAsync();
        }

    }
}
