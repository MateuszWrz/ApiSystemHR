using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemHRApi;
using SystemHRApi.Models;

namespace SystemHRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly EmployeesContext _context;

        public ContractsController(EmployeesContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contracts>>> GetContracts()
        {
          if (_context.Contracts == null)
          {
              return NotFound();
          }
            return await _context.Contracts.ToListAsync();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contracts>> GetContracts(long id)
        {
          if (_context.Contracts == null)
          {
              return NotFound();
          }
            var contracts = await _context.Contracts.FindAsync(id);

            if (contracts == null)
            {
                return NotFound();
            }

            return contracts;
        }

        // PUT: api/Contracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContracts(long id, Contracts contracts)
        {
            if (id != contracts.Id)
            {
                return BadRequest();
            }

            _context.Entry(contracts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractsExists(id))
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

        // POST: api/Contracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contracts>> PostContracts(Contracts contracts)
        {
          if (_context.Contracts == null)
          {
              return Problem("Entity set 'EmployeesContext.Contracts'  is null.");
          }
            _context.Contracts.Add(contracts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContracts", new { id = contracts.Id }, contracts);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContracts(long id)
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }
            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contracts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractsExists(long id)
        {
            return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
