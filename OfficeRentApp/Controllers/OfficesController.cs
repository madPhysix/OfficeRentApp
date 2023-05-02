using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using OfficeRentApp.Helpers;
using OfficeRentApp.Repositories.OfficeRepositories;
using Microsoft.AspNetCore.Authorization;
using OfficeRentApp.DTO;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeRepository _repository;
        public OfficesController(IOfficeRepository repository)
        {
            _repository = repository;
        }
        

        // GET: api/Offices
        [HttpGet]
        public IEnumerable<Office> GetOffices()
        {
          return _repository.GetOffices();
        }

        // GET: api/Offices/5
        [HttpGet("{id}")]
        public IEnumerable<Office> GetOfficesByFilter(string address, decimal minPrice, decimal maxPrice, DateTime checkInTime, int hours)
        {
            return _repository.GetOfficeByFilter(address, minPrice, maxPrice, checkInTime, hours);
        }

        // POST: api/Offices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult PostOffice(UserDto userDto, [FromForm] Office office, IFormFile objfile)
        {
            if (_repository.AddOffice(office, objfile))
                return Ok();
            return Unauthorized();
        }

        // DELETE: api/Offices/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOffice(int id)
        {
            if (_repository.DeleteOffice(id))
                return Ok();
            return Unauthorized();
        }
    }
}
