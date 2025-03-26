using Microsoft.EntityFrameworkCore;
using VehicleRentalSys.Models;


namespace VehicleRentalSys.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

            
        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.User)
        //        .WithMany(u => u.Bookings)
        //        .HasForeignKey(b => b.UserId);

        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.Vehicle)
        //        .WithMany(v => v.Bookings)
        //        .HasForeignKey(b => b.VehicleId);
        //}
    }
}
