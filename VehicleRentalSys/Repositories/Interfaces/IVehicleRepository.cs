using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRentalSys.Models;


namespace VehicleRentalSys.Repositories.Interface
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int vehicleId);
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    }
}
