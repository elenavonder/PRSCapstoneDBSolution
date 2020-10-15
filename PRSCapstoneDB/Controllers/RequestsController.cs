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

        public bool RecalculateRequestTotal(int Id)
        {
            var request = _context.Requests.Find(Id);
            var reqTotal = (from r in _context.RequestLines.ToList()
                            join p in _context.Products.ToList()
                            on r.ProductId equals p.Id
                            where r.RequestId == Id
                            select new
                            {
                                LineTotal = r.Quantity * p.Price
                            }).Sum(t => t.LineTotal);
            request.Total = reqTotal;
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// Reviews the request for the owner(user)
        /// status is set to APPROVED if Total <= 50
        /// else status is set to REVIEW
        /// </summary>
        /// <param name="request">A request</param>
        /// <returns>true is successful; else false</returns>
        public bool ReviewRequest(Request request)
        {
            if (request.Total <= 50)
            {
                request.Status = "APPROVED";
            }
            else
            {
                request.Status = "REVIEWED";
            }
            _context.SaveChanges();
            return true;
        }

        public List <Request> GetRequestsInReview()
        {
            return _context.Requests.Where(r => r.Status == "REVIEW").ToList(); 
        }

        /// <summary>
        /// Sets the status of request to APPROVED
        /// </summary>
        /// <param name="request">a single request</param>
        /// <returns>true if successful; else false</returns>
        public bool SetToApproved(Request request)
        {
            request.Status = "APPROVED";
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// Sets the status of request to REJECTED
        /// </summary>
        /// <param name="request">a single request</param>
        /// <returns>true if successful; else false</returns>
        public bool SetToRejected(Request request)
        {
            request.Status = "REJECTED";
            _context.SaveChanges();
            return true;
        }


        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
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
