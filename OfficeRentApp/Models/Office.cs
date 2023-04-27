using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string? BuildingName { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public decimal PricePerHour { get; set; }
        public List<Rental>? Rental { get; set; } 
        public bool IsEmpty { get; set; } = true; 
        public string? ImagePath { get; set; }
        public Office()
        {

        }

        public Office( string address, int floor, decimal price,string buildingName = "")
        {
            BuildingName = buildingName;
            Floor = floor;
            Address = address;
            PricePerHour = price;
        }
    }
}
