using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSCapstoneDB.Data;
using PRSCapstoneDB.Models;

namespace PRSCapstoneDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSCapstoneContext _context;

        public RequestsController(PRSCapstoneContext context)
        {
            _context = context;
        }
        
        // PUT: api/Requests/Review/id
        [HttpPut("Review/{id}")]
        public async Task<IActionResult> ReviewRequest(int id, Request request)
        {
            request.Status = request.Total <= 50 ? "APPROVED" : "REVIEW";
            return await PutRequest(id, request);
        }

        // PUT: api/Requests/Approve/id
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> SetToApproved(int id, Request request)
        {
            request.Status = "APPROVED";
            return await PutRequest(id, request);
        }
    
        // PUT: api/Requests/Reject/id
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> SetToRejected(int id, Request request)
        {
           request.Status = "REJECTED";
           return await PutRequest(id, request);
        }

        // GET: api/Requests/Review
        [HttpGet("Review")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsInReview()
        {
            return await _context.Requests.Where(r => r.Status == "REVIEW").ToListAsync();
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
