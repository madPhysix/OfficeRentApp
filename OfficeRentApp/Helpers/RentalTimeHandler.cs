using OfficeRentApp.Models;

namespace OfficeRentApp.Helpers
{
    public static class RentalTimeHandler
    {
        public static bool RentTimeHandler(Rental existingRental, Rental requestingRental)
        {
            return requestingRental.EndOfRent < existingRental.StartOfRent || requestingRental.StartOfRent > existingRental.EndOfRent;     
        }
    }
}
