using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using OfficeRentApp.Repositories.RentalRepositories;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        // GET: api/Rentals
        [HttpGet]
        public IEnumerable<Rental> GetRentals()
        {
          return _rentalRepository.GetRentals();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public Rental GetRental(int id)
        {
            return _rentalRepository.GetRental(id);
        }

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostRental(Rental rental)
        {
            _rentalRepository.AddRental(rental);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public void DeleteRental(int id)
        {
            _rentalRepository.DeleteRental(id);
        }

        
        
        
    }
}


