using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.RentalRepositories
{
    public interface IRentalRepository
    {
        public IEnumerable<Rental> GetRentals();
        public Rental GetRental(int id);
        public string AddRental(Rental rental);
        public void DeleteRental(int id);
        public void Save();
        public void ClearData();
    }
}
