using Microsoft.AspNetCore.Mvc;
using OfficeRentApp.DTO;
using OfficeRentApp.Models;

namespace OfficeRentApp.Repositories.OfficeRepositories
{
    public interface IOfficeRepository
    {
        public IEnumerable<Office> GetOffices();
        public Office GetOfficeById(int id);
        public bool AddOffice(UserDto userDto, [FromForm] Office office, IFormFile objfile);
        public bool DeleteOffice(UserDto userDto, int id);
        public void Save();
    }
}
