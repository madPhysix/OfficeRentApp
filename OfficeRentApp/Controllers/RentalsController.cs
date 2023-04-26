using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;

namespace OfficeRentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly OfficeRentDbContext _context;

        public RentalsController(OfficeRentDbContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
          await ClearData();
          if (_context.Rentals == null)
          {
              return NotFound();
          }
          return await _context.Rentals.Include(o => o.Office).ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            await ClearData();
            if (_context.Rentals == null)
          {
              return NotFound();
          }
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }
            return rental;
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            

            if (_context.Rentals == null)
          {
              return Problem("Entity set 'OfficeRentDbContext.Rentals'  is null.");
          }
            await ClearData();
            bool isEmpty = await IsEmpty(rental);
            if (isEmpty == false)
            {
                return BadRequest($"The Office Is busy!");
            }
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalExists(int id)
        {
            return (_context.Rentals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task ClearData()
        {
            List<Rental> rents = _context.Rentals.Include(o => o.Office).Where(x => x.EndOfRent <= DateTime.Now).ToList();
            if (rents.Count > 0)
            {
                foreach (var rental in rents)
                {
                    rental.Office.IsEmpty = true;
                }
                await _context.SaveChangesAsync();
            }
            _context.RemoveRange(rents);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsEmpty(Rental rental)
        {
            var existingRents = await _context.Rentals.Where(x => x.OfficeId == rental.OfficeId).ToListAsync();
            foreach(var rent in existingRents)
            {
                if (!CheckIfEmpty(rental, rent)) return false;
            }
            return true;
        }
        
        private bool CheckIfEmpty(Rental rental, Rental rental2)
        {
            if ((rental2.EndOfRent <= rental.StartOfRent) || (rental2.StartOfRent > rental.EndOfRent))
                return true;
            else return false;
        }
    }
}


