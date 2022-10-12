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
using SystemHRApi.ModelsDTO;

namespace SystemHRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<List<EmployeesDTO>>> GetEmployeeDTO()
        {
            return Ok(await _context.Employee.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployee(long id)
        {
            var employeeItem = await _context.Employee.FindAsync(id);
            if (employeeItem == null)
            {
                return BadRequest("Nie znaleziono pracownika.");
            }
            return Ok(employeeItem);
        }
        // PUT: api/Employees
        [HttpPut]
        public async Task<ActionResult<List<Employees>>> UpdateEmployee(Employees employees)
        {
            var employeeItem = await _context.Employee.FindAsync(employees.Id);
            if (employeeItem == null)
            {
                return BadRequest("Nie znaleziono pracownika.");
            }
            employeeItem.Name = employees.Name;
            employeeItem.Surname = employees.Surname;
            employeeItem.DateOfBirth = employees.DateOfBirth;
            employeeItem.NumberPhone = employees.NumberPhone;
            employeeItem.Adress = employees.Adress;
            employeeItem.DateOfEmployment = employees.DateOfEmployment;
            employeeItem.Contract = employees.Contract;

            await _context.SaveChangesAsync();
            return Ok(await _context.Employee.ToListAsync());

        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<List<Employees>>> PostEmployees(Employees employees)
        {

            _context.Employee.Add(employees);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employee.ToListAsync());
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employees>>> DeleteEmployees(long id)
        {
            var employeeItem = await _context.Employee.FindAsync(id);
            if (employeeItem == null)
            {
                return BadRequest("Nie znaleziono pracownika.");
            }

            _context.Employee.Remove(employeeItem);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employee.ToListAsync());
        }

    }
}
