using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using OfficeRentApp.Helpers;

namespace OfficeRentApp.Repositories.OfficeRepositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly OfficeRentDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ImageManipulation _imageManipulation;
        public OfficeRepository(OfficeRentDbContext context, IWebHostEnvironment webHostEnvironment, ImageManipulation imagemanipulator)
        {
            _context = context;
            _environment = webHostEnvironment;
            _imageManipulation = imagemanipulator;
        }

        public void AddOffice([FromForm] Office office, IFormFile objfile)
        {
            _imageManipulation.ImageAdd(objfile);
            office.ImagePath = "\\Upload\\" + objfile.FileName;
            _context.Offices.Add(office);
            Save();
        }

        public void DeleteOffice(int id)
        {
            var office = _context.Offices.Find(id);
            _context.Offices.Remove(office);
            Save();
        }


        public Office GetOfficeById(int id)
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
