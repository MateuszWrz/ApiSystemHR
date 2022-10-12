using Microsoft.EntityFrameworkCore;
using SystemHRApi.Models;

namespace SystemHRApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employees> Employee => Set<Employees>();
        public DbSet<Contracts> Contract => Set<Contracts>();

    }
}
