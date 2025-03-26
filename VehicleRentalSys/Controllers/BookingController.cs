using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRentalSys.Models;
using VehicleRentalSys.Services.Interface;

namespace VehicleRentalSys.Controllers
{
    //[Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;

        public BookingController(IBookingService bookingService, IVehicleService vehicleService, IUserService userService)
        {
            _bookingService = bookingService;
            _vehicleService = vehicleService;
            _userService = userService;
        }

        private bool IsUser()
        {
            return HttpContext.Session.GetString("UserRole") == "Customer";
        }

        public async Task<IActionResult> GetAllBookings()
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId.Value);
            return View(bookings);
        }

        public async Task<IActionResult> Create()
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");
            await PopulateDropdowns();
            return View(new Booking());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            booking.UserId = userId.Value;

            var vehicle = await _vehicleService.GetVehicleByIdAsync(booking.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                ModelState.AddModelError("", "Selected vehicle is not available for booking.");
                await PopulateDropdowns();
                return View(booking);
            }

            if (booking.EndDate <= booking.StartDate)
            {
                ModelState.AddModelError("", "End date must be after the start date.");
                await PopulateDropdowns();
                return View(booking);
            }

            int totalDays = (booking.EndDate - booking.StartDate).Days;
            booking.TotalAmount = totalDays * vehicle.RentalPricePerDay;

            vehicle.IsAvailable = false;
            await _vehicleService.UpdateVehicleAsync(vehicle);
            await _bookingService.AddBookingAsync(booking);

            return RedirectToAction(nameof(GetAllBookings));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return booking == null ? NotFound() : View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");

            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            var vehicle = await _vehicleService.GetVehicleByIdAsync(booking.VehicleId);
            if (vehicle != null)
            {
                vehicle.IsAvailable = true;
                await _vehicleService.UpdateVehicleAsync(vehicle);
            }

            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction(nameof(GetAllBookings));
        }

        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }


        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Load available vehicles
            await PopulateDropdowns();

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null || booking.UserId != userId.Value)
            {
                return NotFound();
            }

            await PopulateDropdowns();
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (!IsUser()) return RedirectToAction("AccessDenied", "Home");

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            var existingBooking = await _bookingService.GetBookingByIdAsync(id);
            if (existingBooking == null || existingBooking.UserId != userId.Value)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(booking.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                ModelState.AddModelError("", "Selected vehicle is not available.");
                await PopulateDropdowns();
                return View(booking);
            }

            if (booking.EndDate <= booking.StartDate)
            {
                ModelState.AddModelError("", "End date must be after the start date.");
                await PopulateDropdowns();
                return View(booking);
            }

            int totalDays = (booking.EndDate - booking.StartDate).Days;
            booking.TotalAmount = totalDays * vehicle.RentalPricePerDay;

            if (ModelState.IsValid)
            {
                await _bookingService.UpdateBookingAsync(booking);
                return RedirectToAction(nameof(GetAllBookings));
            }

            await PopulateDropdowns();
            return View(booking);
        }



        private async Task PopulateDropdowns()
        {
            var availableVehicles = (await _vehicleService.GetAllVehiclesAsync())?.Where(v => v.IsAvailable).ToList() ?? new List<Vehicle>();
            ViewBag.VehicleId = new SelectList(availableVehicles, "VehicleId", "Brand");
        }



    }
}
