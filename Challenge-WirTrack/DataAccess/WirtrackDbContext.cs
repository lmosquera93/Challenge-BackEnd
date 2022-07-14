using Challenge_WirTrack.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge_WirTrack.DataAccess
{
    public class WirtrackDbContext : DbContext
    {
        public WirtrackDbContext(DbContextOptions<WirtrackDbContext> options) : base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Travel> Travels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CitySeeds();
            modelBuilder.VehicleSeeds();
            modelBuilder.TravelSeeds();

        }
    }
}
