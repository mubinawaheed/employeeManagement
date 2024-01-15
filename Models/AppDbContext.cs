using Microsoft.EntityFrameworkCore;

namespace employeeManagement.Models

{
	public class AppDbContext : DbContext
	{

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }
        //we will use this property to query and save instances of the employee class
        public DbSet<Employee> Employees { get; set; }

    }
}
