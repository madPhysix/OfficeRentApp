using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using OfficeRentApp.Helpers;
using OfficeRentApp.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OfficeRentApp.Repositories.OfficeRepositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficeRentDbContext _context;
        private readonly ImageManipulation _imageManipulation;
        public OfficeRepository(OfficeRentDbContext context,ImageManipulation imagemanipulator)
        {
            _context = context;
            _imageManipulation = imagemanipulator;
        }

        public bool AddOffice(UserDto userDto, [FromForm] Office office, IFormFile objfile)
        {
            if (userDto.Role == "User")
                return false;
            _imageManipulation.ImageAdd(objfile);
            office.ImagePath = "\\Upload\\" + objfile.FileName;
            _context.Offices.Add(office);
            Save();
            return true;
        }

        public bool DeleteOffice(UserDto userDto, int id)
        {
            if (userDto.Role == "User")
                return false;
            var office = _context.Offices.Find(id);
            _context.Offices.Remove(office);
            Save();
            return true;
        }


        public Office GetOfficeFilter()
        {
                return _context.Offices.Find(id);
        }

        public IEnumerable<Office> GetOffices()
        {
            return _context.Offices.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
