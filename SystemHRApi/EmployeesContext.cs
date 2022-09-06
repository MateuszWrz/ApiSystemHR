using Microsoft.EntityFrameworkCore;
using SystemHRApi.Models;
namespace SystemHRApi
{
    public class EmployeesContext:DbContext
    {
        public EmployeesContext(DbContextOptions<EmployeesContext> options)
            : base(options)
        {

        }
        public DbSet<Employees> Employee { get; set; } = null!;
    }
}
