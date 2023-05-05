using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Helpers;
using OfficeRentApp.Models;
using System.Security.Cryptography;

namespace OfficeRentApp.Data
{
    public class OfficeRentDbContext:DbContext
    {
        public OfficeRentDbContext(DbContextOptions<OfficeRentDbContext> context): base(context)
        {

        }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleDefine> Roles { get; set; }
        public DbSet<ImageManipulation> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Office>().HasData
                (
                    new Office
                    {
                        Id = 1,
                        BuildingName = "Caspian Plaza",
                        Address = "44 Jafar Jabbarli street, Baku 1065",
                        Floor = 8,
                        PricePerHour = 60,
                        Description = "bomba ofisdi",
                        HasAC = true,
                        HasCoffeeService = true,
                        HasParking = true,
                        HasWifi = true,
                        Location = "40.3853494919391, 49.828683540862414"
                    },
                    new Office
                    {
                        Id = 2,
                        BuildingName = "Caspian Plaza",
                        Address = "44 Jafar Jabbarli street, Baku 1065",
                        Floor = 15,
                        PricePerHour = 85,
                        Description = "bomba ofisdi",
                        HasAC = true,
                        HasCoffeeService = true,
                        HasParking = true,
                        HasWifi = true,
                        Location = "40.3853494919391, 49.828683540862414"
                    },
                    new Office
                    {
                        Id = 3,
                        BuildingName = "Caspian Plaza",
                        Address = "44 Jafar Jabbarli street, Baku 1065",
                        Floor = 3,
                        PricePerHour = 40,
                        Description = "bomba ofisdi",
                        HasAC = true,
                        HasCoffeeService = true,
                        HasParking = true,
                        HasWifi = true,
                        Location = "40.3853494919391, 49.828683540862414"
                    }
                );
            modelbuilder.Entity<UserRoleDefine>().HasData
                (
                    new UserRoleDefine
                    {
                        Id = 1,
                        Role = "User"
                    },
                    new UserRoleDefine
                    {
                        Id = 2,
                        Role = "Admin"
                    }
                );
        }
    }
}
