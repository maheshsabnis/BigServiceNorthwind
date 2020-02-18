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
    public class OrderDetailsDataController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public OrderDetailsDataController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetailsData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailsData>>> GetOrderDetailsData()
        {
            return await _context.OrderDetailsData.ToListAsync();
        }

        // GET: api/OrderDetailsData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsData>> GetOrderDetailsData(int id)
        {
            var orderDetailsData = await _context.OrderDetailsData.FindAsync(id);

            if (orderDetailsData == null)
            {
                return NotFound();
            }

            return orderDetailsData;
        }

        // PUT: api/OrderDetailsData/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetailsData(int id, OrderDetailsData orderDetailsData)
        {
            if (id != orderDetailsData.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetailsData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsDataExists(id))
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

        // POST: api/OrderDetailsData
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderDetailsData>> PostOrderDetailsData(OrderDetailsData orderDetailsData)
        {
            _context.OrderDetailsData.Add(orderDetailsData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailsDataExists(orderDetailsData.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetailsData", new { id = orderDetailsData.OrderId }, orderDetailsData);
        }

        // DELETE: api/OrderDetailsData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetailsData>> DeleteOrderDetailsData(int id)
        {
            var orderDetailsData = await _context.OrderDetailsData.FindAsync(id);
            if (orderDetailsData == null)
            {
                return NotFound();
            }

            _context.OrderDetailsData.Remove(orderDetailsData);
            await _context.SaveChangesAsync();

            return orderDetailsData;
        }

        private bool OrderDetailsDataExists(int id)
        {
            return _context.OrderDetailsData.Any(e => e.OrderId == id);
        }
    }
}
