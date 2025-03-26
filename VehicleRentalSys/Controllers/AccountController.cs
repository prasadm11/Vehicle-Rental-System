using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VehicleRentalSys.Data;
using VehicleRentalSys.Models;

namespace VehicleRentalSys.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {


                HttpContext.Session.SetInt32("UserId", user.UserId); // Ensure this is called
                HttpContext.Session.SetString("UserRole", user.Role); // Store user role

                Console.WriteLine($"User Logged In: ID = {user.UserId}, Role = {user.Role}");


                if (user.Role == "Admin")
                    return RedirectToAction("GetAllVehicles", "Vehicle");
                else
                    return RedirectToAction("GetAllBookings", "Booking");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
