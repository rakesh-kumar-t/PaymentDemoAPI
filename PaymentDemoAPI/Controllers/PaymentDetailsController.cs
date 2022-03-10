using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentDemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : Controller
    {
        private readonly PaymentDetailsContext _context;
        public PaymentDetailsController(PaymentDetailsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            return paymentDetail;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPaymentDetail), new { id = paymentDetail.PaymentDetailID },paymentDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDetail>> PutPaymentDetail(int id,PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailID)
            {
                return BadRequest();
            }
            _context.Entry(paymentDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExist(id))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PaymentDetailExist(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailID == id);
        }
    }
}
