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
        public async Task<bool> Add(Car car)
        {
            _context.Add(car);
            return await Save();
        }

        public async Task<bool> Delete(Car car)
        {
            _context.Remove(car);
            return await Save();
        }


        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.Where(car => car.IsActive).ToListAsync();
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

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> Update(Car car)
        {
            _context.Update(car);
            return await Save();
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
        public async Task<IEnumerable<Car>> GetFilteredCars(string city, string type, string makeModel, string sortOrder)
        {
            var query = _context.Cars.Where(car => car.IsActive == true).AsQueryable();



            if (!string.IsNullOrEmpty(type) && Enum.TryParse(type, true, out CarType carTypeEnum))
            {
                query = query.Where(car => car.carType == carTypeEnum);
            }

            if (!string.IsNullOrEmpty(makeModel))
            {
                var parts = makeModel.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    var temp = part.ToLower();
                    query = query.Where(car => car.Make.ToLower().Contains(temp) || car.Model.ToLower().Contains(temp));
                }
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(car => car.City.Contains(city));
            }
            switch (sortOrder)
            {
                case "PriceAsc":
                    query = query.OrderBy(car => car.DailyRate);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(car => car.DailyRate);
                    break;
                case "MakeAsc":
                    query = query.OrderBy(car => car.Make);
                    break;
                case "MakeDesc":
                    query = query.OrderByDescending(car => car.Make);
                    break;
                default:
                    query = query.OrderBy(car => car.CarId);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<List<Car>> GetUnAvailableCarsAsyncToUpdate()
        {
            var today = DateTime.Today;
            //return await _context.Cars.FromSqlRaw("EXEC GetUnAvailableCarsToUpdate").ToListAsync();
            return await _context.Cars.Where(c => !c.IsAvailable && _context.Rental.Where(r => r.CarId == c.CarId)
            .OrderByDescending(r => r.EndDate).Select(r => r.EndDate).FirstOrDefault() <= today).ToListAsync();

            //return cars;
        }
        public async Task<List<Car>> GetUnAvailableCarsAsync()
        {
            //return await _context.Cars.FromSqlRaw("EXEC GetUnavailableCars").ToListAsync();
            return await _context.Cars.Where(c => !c.IsAvailable).ToListAsync();
        }

        public async Task<List<string>> GetCarsCities()
        {
            return await _context.Cars.Where(c => c.IsAvailable).Select(c => c.City).Distinct().ToListAsync();
        }

        public async Task<int> GetCarsCountAsync()
        {
            return await _context.Cars.CountAsync();
        }
        //public async Task<List<Car>> GetSortedCars(string sort)
        //{
        //    return await _context.Cars.FromSqlRaw("EXEC GetCarsSortedByPrice @SortDirection = {0}", sort).ToListAsync();
        //}
    }
}
