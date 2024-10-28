using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
    }
}
