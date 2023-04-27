using Microsoft.AspNetCore.Mvc;
using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.OfficeRepositories
{
    public interface IOfficeRepository
    {
        public IEnumerable<Office> GetOffices();
        public Office GetOfficeById(int id);
        public void AddOffice([FromForm] Office office, IFormFile objfile);
        public void DeleteOffice(int id);
        public void Save();
    }
}
