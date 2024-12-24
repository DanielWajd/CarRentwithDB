using CarRentwithDB.Data.Enum;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Services
{
    public class CarRepository : ICarService
    {
        private readonly CarRentDBContext _context;

        public CarRepository(CarRentDBContext context)
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
        public async Task<IEnumerable<Car>> GetFilteredCars(string city, string type, string makeModel)
        {
            var query = _context.Cars.AsQueryable();

            

            if (!string.IsNullOrEmpty(type) && Enum.TryParse(type, true, out CarType carTypeEnum))
            {
                query = query.Where(car => car.carType == carTypeEnum);
            }
            if (!string.IsNullOrEmpty(makeModel))
            {
                var parts = makeModel.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    var make = parts[0];
                    var model = string.Join(" ", parts.Skip(1));

                    query = query.Where(car => car.Make.Contains(make) && car.Model.Contains(model));
                }
                else
                {
                    var search = parts[0];
                    query = query.Where(car => car.Make.Contains(search) || car.Model.Contains(search));
                }

            }
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(car => car.City.Contains(city));
            }
            return await query.ToListAsync();
        }

        public async Task<List<Car>> GetUnAvailableCarsAsync()
        {
            return await _context.Cars.FromSqlRaw("EXEC GetUnAvailableCars").ToListAsync();
        }
        public async Task<List<Car>> GetSortedCars(string sort)
        {
            return await _context.Cars.FromSqlRaw("EXEC GetCarsSortedByPrice @SortDirection = {0}", sort).ToListAsync();
        }
    }
}
