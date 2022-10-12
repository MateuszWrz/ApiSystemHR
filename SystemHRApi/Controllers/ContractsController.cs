using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemHRApi;
using SystemHRApi.Data;
using SystemHRApi.Models;

namespace SystemHRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly DataContext _context;

        public ContractsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contracts>>> GetContracts()
        {
          if (_context.Contract == null)
          {
              return NotFound();
          }
            return await _context.Contract.ToListAsync();
        }

        // POST: api/Contracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contracts>> PostContracts(Contracts contracts)
        {
          if (_context.Contract == null)
          {
              return Problem("Entity set 'EmployeesContext.Contracts'  is null.");
          }
            _context.Contract.Add(contracts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContracts", new { id = contracts.Id }, contracts);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContracts(long id)
        {
            if (_context.Contract == null)
            {
                return NotFound();
            }
            var contracts = await _context.Contract.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }

            _context.Contract.Remove(contracts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractsExists(long id)
        {
            return (_context.Contract?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
