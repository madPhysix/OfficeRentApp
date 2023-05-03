using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using OfficeRentApp.Helpers;
using OfficeRentApp.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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

        
        public bool AddOffice([FromForm] Office office, IFormFile objfile)
        {
            _imageManipulation.ImageAdd(objfile);
            office.ImagePath = "\\Upload\\" + objfile.FileName;
            _context.Offices.Add(office);
            Save();
            return true;
        }

        [Authorize(Roles = "Admin")]
        public bool DeleteOffice(int id)
        {
            
            var office = _context.Offices.Find(id);
            if (office != null)
            {
                _context.Offices.Remove(office);
                Save();
            }
            return true;
        }


        public IEnumerable<Office> GetOfficeByFilter(string address, decimal? minPrice, decimal? maxPrice, DateTime? checkInTime, int? hours)
        {
            /*return _context.Offices
                   .Where(x => x.Address.Contains(address) && x.PricePerHour >= minPrice && x.PricePerHour <= maxPrice
                    && (x.Rentals.Any(r => r.StartOfRent > checkInTime.AddHours(hours) || checkInTime > r.EndOfRent) || x.Rentals.Count == 0));*/

            var query = _context.Offices.AsQueryable();

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(d => d.Address.Contains(address));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(d => d.PricePerHour >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(d => d.PricePerHour <= maxPrice);
            }

            if (checkInTime.HasValue && hours.HasValue)
            {
                DateTime updatedDate = checkInTime.Value;
                int updatedHours = hours.Value;
                query = query.Where(x => x.Rentals.Any(r => r.StartOfRent > updatedDate.AddHours(updatedHours) || updatedDate > r.EndOfRent) || x.Rentals.Count == 0);
            }

            return query.ToList();
        }

        public IEnumerable<Office> GetOffices()
        {
            return _context.Offices.Include(o => o.Rentals);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
