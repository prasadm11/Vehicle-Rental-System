using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRentalSys.Models;

namespace VehicleRentalSys.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
