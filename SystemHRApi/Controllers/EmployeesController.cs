using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemHRApi;
using SystemHRApi.Models;
using SystemHRApi.ModelsDTO;

namespace SystemHRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesContext _context;

        public EmployeesController(EmployeesContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesDTO>>> GetEmployeeDTO()
        {
            return await _context.Employee
                .Select(x => EmployeeToDTO(x))
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeesDTO>> GetEmployeesDTO(long id)
        {
            var employeeItem = await _context.Employee.FindAsync();

            if (employeeItem == null)
            {
                return NotFound();
            }

            return EmployeeToDTO(employeeItem);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees(long id, EmployeesDTO employeesDTO)
        {
            if (id != employeesDTO.Id)
            {
                return BadRequest();
            }

            var employeeItem = await _context.Employee.FindAsync();
            if (employeeItem == null)
            {
                return NotFound();
            }

            employeeItem.Name = employeesDTO.Name;
            employeeItem.Surname = employeesDTO.Surname;
            employeeItem.Adress = employeesDTO.Adress;
            employeeItem.NumberPhone = employeesDTO.NumberPhone;
            employeeItem.DateOfBirth = employeesDTO.DateOfBirth;
            employeeItem.DateOfEmployment = employeesDTO.DateOfEmployment;
            employeeItem.Contract = employeesDTO.Contract;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EmployeesExists(id))
            {
                return NotFound();

            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(EmployeesDTO employeesDTO)
        {
            var employeeItem = new Employees
            {
                Name = employeesDTO.Name,
                Surname = employeesDTO.Surname,
                Adress = employeesDTO.Adress,
                NumberPhone = employeesDTO.NumberPhone,
                DateOfBirth = employeesDTO.DateOfBirth,
                DateOfEmployment = employeesDTO.DateOfEmployment,
                Contract = employeesDTO.Contract,
            };
            _context.Employee.Add(employeeItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetEmployees", new { id = employees.Id }, employees);
            return CreatedAtAction(nameof(GetEmployeesDTO), new { id = employeeItem.Id }, EmployeeToDTO(employeeItem));
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees(long id)
        {
            var employeeItem = await _context.Employee.FindAsync(id);
            if (employeeItem == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employeeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeesExists(long id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private static EmployeesDTO EmployeeToDTO(Employees employees) =>
            new EmployeesDTO
            {
                Id = employees.Id,
                Name = employees.Name,
                Surname = employees.Surname,
                Adress = employees.Adress,
                NumberPhone = employees.NumberPhone,
                DateOfBirth = employees.DateOfBirth,
                DateOfEmployment = employees.DateOfEmployment,
                Contract = employees.Contract
            };
    }
}
