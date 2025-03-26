using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRentalSys.Models;


namespace VehicleRentalSys.Services.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int vehicleId);
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    }
}
