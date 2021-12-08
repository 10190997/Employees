using Microsoft.EntityFrameworkCore;

namespace Employees.API.Models
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        public EmployeesContext(DbContextOptions<EmployeesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}