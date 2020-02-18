using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigServiceNorthwind.Models;

namespace BigServiceNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersDataController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public SuppliersDataController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/SuppliersData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuppliersData>>> GetSuppliersData()
        {
            return await _context.SuppliersData.ToListAsync();
        }

        // GET: api/SuppliersData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuppliersData>> GetSuppliersData(double id)
        {
            var suppliersData = await _context.SuppliersData.FindAsync(id);

            if (suppliersData == null)
            {
                return NotFound();
            }

            return suppliersData;
        }

        // PUT: api/SuppliersData/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuppliersData(double id, SuppliersData suppliersData)
        {
            if (id != suppliersData.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(suppliersData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersDataExists(id))
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

        // POST: api/SuppliersData
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SuppliersData>> PostSuppliersData(SuppliersData suppliersData)
        {
            _context.SuppliersData.Add(suppliersData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SuppliersDataExists(suppliersData.SupplierId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSuppliersData", new { id = suppliersData.SupplierId }, suppliersData);
        }

        // DELETE: api/SuppliersData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuppliersData>> DeleteSuppliersData(double id)
        {
            var suppliersData = await _context.SuppliersData.FindAsync(id);
            if (suppliersData == null)
            {
                return NotFound();
            }

            _context.SuppliersData.Remove(suppliersData);
            await _context.SaveChangesAsync();

            return suppliersData;
        }

        private bool SuppliersDataExists(double id)
        {
            return _context.SuppliersData.Any(e => e.SupplierId == id);
        }
    }
}
