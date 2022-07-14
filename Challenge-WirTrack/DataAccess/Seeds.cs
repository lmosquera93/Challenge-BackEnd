using Challenge_WirTrack.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Challenge_WirTrack.DataAccess
{
    public static class Seeds
    {
        public static void CitySeeds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    IsDeleted = false,
                    Name = @"Buenos Aires",
                    LastModified = DateTime.Now
                },
                new City
                {
                    Id = 2,
                    IsDeleted = false,
                    Name = @"Mar del Plata",
                    LastModified = DateTime.Now
                },
                new City
                {
                    Id = 3,
                    IsDeleted = false,
                    Name = @"La Plata",
                    LastModified = DateTime.Now
                });
        }

        public static void VehicleSeeds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = 1,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Type = @"Car",
                    Patent = @"AAA000",
                    Brand = @"Toyota"
                },
                new Vehicle
                {
                    Id = 2,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Type = @"Truck",
                    Patent = @"AAA001",
                    Brand = @"Honda"
                },
                new Vehicle
                {
                    Id = 3,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Type = @"Truck",
                    Patent = @"AAA003",
                    Brand = @"Scannia"
                });
        }

        public static void TravelSeeds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Travel>().HasData(
                new Travel
                {
                    Id = 1,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today,
                    CityID = 1,
                    VehicleID = 1
                    
                },
                new Travel
                {
                    Id = 2,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today.AddDays(1),
                    CityID = 2,
                    VehicleID = 1
                    
                },
                new Travel
                {
                    Id = 3,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today.AddDays(2),
                    CityID = 3,
                    VehicleID = 1
                    
                },
                new Travel
                {
                    Id = 4,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today,
                    CityID = 1,
                    VehicleID = 2
                    
                },
                new Travel
                {
                    Id = 5,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today.AddDays(1),
                    CityID = 2,
                    VehicleID = 2
                    
                },
                new Travel
                {
                    Id = 6,
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    Date = DateTime.Today.AddDays(2),
                    CityID = 3,
                    VehicleID = 2
                    
                });
        }
    }
}
