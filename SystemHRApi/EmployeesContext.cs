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
        public DbSet<SystemHRApi.Models.Contracts>? Contracts { get; set; }
    }
}
