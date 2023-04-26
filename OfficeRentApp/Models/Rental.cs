using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class Rental
    {
        public Rental()
        {

        }
        public Rental(DateTime startOfRent, DateTime endOfRent, int officeId)
        {
            StartOfRent = startOfRent;
            EndOfRent = endOfRent;
            OfficeId = officeId;
        }
        public int Id { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        [ForeignKey("Office")]
        public int OfficeId { get; set; }
        public Office? Office { get; set; }
    }
}
