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
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsCapstoneBackEndContext _context;

        public RequestLinesController(PrsCapstoneBackEndContext context)
        {
            _context = context;
        }

        /*ADDITIONAL METHOD  JUST ONE 
   RecalculateRequestTotal(requestId) : [PRIVATE] - Recalculates the Total property whenever an insert, update, 
  or delete occurs to the Requestlines attached to the request. 
  This method is private and cannot be called from outside the class. 
  It should still be executed asynchronously. It should be called from the PUT, POST, and DELETE methods
  only AFTER the SaveChangesAsync() is called in those methods.
    FROM PoWeb
    //below is method from thur 6-3 homework tuff method
      //recalculatepototal is name we chose for method, pass thru POId , variable name we choose is POID  po id
  //l is var for lines, x is fred var

      private async Task RecalculatePoTotal(int POID)
      {
          var po = await _context.POs.FindAsync(POID);
          if (po == null) throw new Exception("FAtal: PO is not found to recalc!");
          var poTotal = (from l in _context.polines
                        join i in _context.Items
                        on l.ItemId equals i.ID
                        where l.POId == POID
                        select new { LineTotal = l.Quantity * i.Price})
                       .Sum( x => x.LineTotal);
          po.Total = poTotal;
          await _context.SaveChangesAsync();
   */
        private async Task RecalculateRequestTotal(int RequestId)
        {
            var requ = await _context.Request.FindAsync(RequestId);
            if (Request == null) throw new Exception("Fatal: Request is not found to recalculate!");
            var requTotal = (from l in _context.RequestLine
                             join i in _context.Product
                             on l.ProductId equals i.Id
                             where l.RequestId  == RequestId
                             select new { LineTotal = l.Quantity * i.Price })
                             .Sum(x => x.LineTotal);
            requ.Total = requTotal;
            await _context.SaveChangesAsync();
        }

        /* GET: api/Products   added lambda in parens ---include( x=>x.Vendor).ToListAsync(); to tie product FK 
         * to pulling the vendor name in data Get Example from Products get:   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    { return await _context.Product.Include( x=>x.Vendor).ToListAsync(); }
*/
        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLine()
        {
            return await _context.RequestLine.Include(x => x.Product).ToListAsync();
        }

        //ADDED LAMBDA BELOW
        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            var requestLine = await _context.RequestLine.Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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

        // POST: api/RequestLines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine)
        {
            _context.RequestLine.Add(requestLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLine>> DeleteRequestLine(int id)
        {
            var requestLine = await _context.RequestLine.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            _context.RequestLine.Remove(requestLine);
            await _context.SaveChangesAsync();

            return requestLine;
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLine.Any(e => e.Id == id);
        }
    }
}
