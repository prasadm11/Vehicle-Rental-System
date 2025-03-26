using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VehicleRentalSys.Models;
using VehicleRentalSys.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace VehicleRentalSys.Controllers
{
    //[Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // Restrict access to only Admin
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        public async Task<IActionResult> GetAllVehicles()
        {
            if (!IsAdmin()) 
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);
        }

        public async Task<IActionResult> GetVehicleById(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (ModelState.IsValid)
            {
                await _vehicleService.AddVehicleAsync(vehicle);
                return RedirectToAction(nameof(GetAllVehicles));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _vehicleService.UpdateVehicleAsync(vehicle);
                return RedirectToAction(nameof(GetAllVehicles));
            }

            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            await _vehicleService.DeleteVehicleAsync(id);
            return RedirectToAction(nameof(GetAllVehicles));
        }
    }
}

