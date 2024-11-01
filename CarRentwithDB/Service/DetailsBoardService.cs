using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;

namespace CarRentwithDB.Service
{
    public class DetailsBoardService : IDetailsBoardService
    {
        private readonly CarRentDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsBoardService(CarRentDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Car>> GetAllCars()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userCars = _context.Cars.Where(r => r.AppUser.Id == curUser);
            return userCars.ToList();
        }
    }
}
