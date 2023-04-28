using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Models;
using OfficeRentApp.Models.UserModels;
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
                        PricePerHour = 60
                    },
                    new Office
                    {
                        Id = 2,
                        BuildingName = "Caspian Plaza",
                        Address = "44 Jafar Jabbarli street, Baku 1065",
                        Floor = 15,
                        PricePerHour = 85
                    },
                    new Office
                    {
                        Id = 3,
                        BuildingName = "Caspian Plaza",
                        Address = "44 Jafar Jabbarli street, Baku 1065",
                        Floor = 3,
                        PricePerHour = 40
                    }
                );
        }
    }
}
