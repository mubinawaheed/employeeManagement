namespace employeeManagement.Models
{

	//creating an extension method
	public static class ModelBuilderExtensions
	{
		public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>().HasData(
					new Employee
					{
						Name = "Mubina",
						Id = 1,
						Department = Dept.IT,
						Email = "mubina@gmail.com"
					},

					new Employee
					{
						Name = "Ali",
						Id = 2,
						Department = Dept.IT,
						Email = "ali@gmail.com"
					}
					);
		}
	}
}
