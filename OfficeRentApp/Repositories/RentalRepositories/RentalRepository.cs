using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
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
        public void AddRental(Rental rental)
        {
            ClearData();
            _context.Rentals.Add(rental);
            Save();
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
            var rentals = _context.Rentals.ToList();
                return rentals;
        }

        public void Save()
        {
            ClearData();
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
