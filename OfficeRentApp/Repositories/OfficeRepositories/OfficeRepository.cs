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

        public IEnumerable<Office> GetOfficesByIds(int[] ids)
        {
            return _context.Offices.Where(o => ids.Contains(o.Id));
        }


        public bool AddOffice([FromForm] Office office, IEnumerable<IFormFile> objfiles)
        {
            _context.Offices.Add(office);
            Save();
            _imageManipulation.ImageAdd(office.Id ,objfiles);
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


        public IEnumerable<Office> GetOfficeByFilter(string? address, decimal? minPrice, decimal? maxPrice, DateTime? checkInTime, int? hours)
        {
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

            return query.Include(i => i.Images).ToList();
        }

        public IEnumerable<Office> GetOffices()
        {
            return _context.Offices.Include(o => o.Rentals).Include(i => i.Images);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
