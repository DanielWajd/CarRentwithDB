using CarRentwithDB.Models;

namespace CarRentwithDB.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetIdByNoTracking(string id);
        Task<bool> UpdateUser(AppUser user);
        Task<bool> Save();
        Task<bool> VerifyPasswordAsync(AppUser user, string currentPassword);
        Task<bool> UpdatePasswordAsync(AppUser user, string newPassword);
        Task<IEnumerable<AppUser>> GetFilteredUsers(string name, string surname, string email, string phone);
        Task<int> GetUsersCountAsync();
    }
}
