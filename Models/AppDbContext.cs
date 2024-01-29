using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace employeeManagement.Models

{
	public class AppDbContext : IdentityDbContext
	{

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }
        //we will use this property to query and save instances of the employee class
        public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Seed();	
				
		}

	}
}
