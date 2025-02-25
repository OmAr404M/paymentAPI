using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paymentAPI.Models;

namespace paymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/PymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PymentDetail>> GetPymentDetail(int id)
        {
            var pymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (pymentDetail == null)
            {
                return NotFound();
            }

            return pymentDetail;
        }

        // PUT: api/PymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPymentDetail(int id, PymentDetail pymentDetail)
        {
            if (id != pymentDetail.PaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(pymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PymentDetailExists(id))
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

        // POST: api/PymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PymentDetail>> PostPymentDetail(PymentDetail pymentDetail)
        {
            if(_context.PaymentDetails == null)
            {
                return Problem("Entity setr 'PaymentDetaul.PaymentDetais' is null ");
            }
            _context.PaymentDetails.Add(pymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPymentDetail", new { id = pymentDetail.PaymentDetailId }, pymentDetail);
        }

        // DELETE: api/PymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePymentDetail(int id)
        {
            var pymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (pymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(pymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }
    }
}
