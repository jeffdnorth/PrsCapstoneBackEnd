using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsCapstoneBackEnd.Data;
using PrsCapstoneBackEnd.Models;

namespace PrsCapstoneBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsCapstoneBackEndContext _context;

        public RequestsController(PrsCapstoneBackEndContext context)
        {
            _context = context;
        }

        /*
       4. GetReviews(userId) : [GET: /api/requests/review/{id}] - Gets requests in "REVIEW" status and not owned by the user with the primary key of id.
          //FROM PoWeb
             // GET: api/POes GET ALL THE POS WHERE STATUS IS SET TO REVIEW
        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<PO>>> GetPOsinReview()
        {
            return await _context.POs
                    .Where(p => p.Status == PO.StatusReview)
                    .Include(p => p.Employee)
                    .ToListAsync(); 
        }
       */
        // 4. GetReviews(userId) : [GET: /api/requests/review/{id}] - Gets requests in "REVIEW" status and not owned by the user with the primary key of id.
        [HttpGet("requests")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsinReview()
        {
            return await _context.Request
                    .Where(r => r.Status == Request.StatusIsReview)
                    .Include(r => ! r.Id)
                    .ToListAsync();
        }


        /* GET: api/Products   added lambda in parens ---include( x=>x.Vendor).ToListAsync();
   // -- to tie product FK to pulling the vendor name in data Get
         Example from Products get:
    * //     
   [HttpGet]
   public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
   {
       return await _context.Product.Include( x=>x.Vendor).ToListAsync();
   }
    */
        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.Include(x => x.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //1. Review(request) : [PUT: /api/requests/5/review] - Sets the status of the request for the id provided to "REVIEW" 
        // unless the total of the request is less than or equal to $50. If so, it sets the status directly to "APPROVED".
        [HttpPut("{id}/review")]
        public async Task<IActionResult> PutStatusReviewOrApproved(int id, Request request)
        {
            var requ = await _context.Request.FindAsync(id);
            if (Request == null)
            {
                return NotFound();
            }
            requ.Status = (requ.Total <= 50) ? "APPROVE" : "REVIEW";
            return await PutRequest(id, requ);
        }

        // 2. Approve(request) : [PUT: /api/requests/5/approve] - Sets the status of the request for the id provided to "APPROVED"
        [HttpPut("{id)}/approve")]
        public async Task<IActionResult> PutStatusToApprove(int id, Request request)
        {
            
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "APPROVE";
            return await PutRequest(id, request);
        }

        //3. Reject(request) : [PUT: /api/requests/5/reject] - Sets the status of the request for the id provided to "REJECTED"
                     // PUT method for reject

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> PutStatusToReject(int id, Request request)
        {
            
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "Reject";
            return await PutRequest(id, request);
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
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
