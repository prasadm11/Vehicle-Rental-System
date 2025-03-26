using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalSys.Models
{
    public class Vehicle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }  

        [Required]
        [Range(0, double.MaxValue)]
        public decimal RentalPricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation Property: One Vehicle can have multiple Bookings
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}