using Microsoft.AspNetCore.Mvc;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.OfficeRepositories
{
    public interface IOfficeRepository
    {
        public IEnumerable<Office> GetOffices();
        public IEnumerable<Office> GetOfficeByFilter(string address, decimal minPrice, decimal maxPrice, DateTime checkInTime, int hours);
        public bool AddOffice([FromForm] Office office, IFormFile objfile);
        public bool DeleteOffice(int id);
        public void Save();
    }
}
