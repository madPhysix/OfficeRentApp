using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Helpers;
using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.RentalRepositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly OfficeRentDbContext _context;
        
        public RentalRepository(OfficeRentDbContext context) 
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,User")]
        public string AddRental(Rental rental)
        {
            ClearData();
            var existingRentals = _context.Rentals.Where(r => r.OfficeId == rental.OfficeId).ToList();
                foreach (var existingRental in existingRentals)
                {
                    if (RentalTimeHandler.RentTimeHandler(existingRental, rental)) continue;
                    else return $"The Office is busy from {existingRental.StartOfRent.ToString("dddd HH:mm")} " +
                        $"till {existingRental.EndOfRent.ToString("dddd HH:mm")}";
                }
            
            _context.Rentals.Add(rental);
            Save();
            return "Registered!";
        }

        public void DeleteRental(int id)
        {
            var deletingRental = _context.Rentals.Find(id);
            if (deletingRental != null)
            {
                _context.Rentals.Remove(deletingRental);
                Save();
            }
        }

        public Rental GetRental(int id)
        {
            ClearData();
            return _context.Rentals.Find(id);
        }

        public IEnumerable<Rental> GetRentals()
        {
            ClearData();
            return _context.Rentals.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void ClearData()
        {
            List<Rental> rents = _context.Rentals.Include(o => o.Office).Where(x => x.EndOfRent <= DateTime.Now).ToList();
            _context.RemoveRange(rents);
            _context.SaveChanges();
        }

    }
}
