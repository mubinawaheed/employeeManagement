
namespace employeeManagement.Models
{
	public class SQLEmployeeRepository : IEmployeeRepository
	{
		private readonly AppDbContext context;
		private readonly ILogger<SQLEmployeeRepository> logger;

		public SQLEmployeeRepository(AppDbContext context, ILogger<SQLEmployeeRepository> logger)
        {
			this.context = context;
			this.logger = logger;
		}

		public Employee AddEmployee(Employee employee)
		{
			context.Employees.Add(employee);
			context.SaveChanges();
			return employee;
		}

		public Employee Delete(int id)
		{
			Employee employee = context.Employees.Find(id);
			if(employee!= null)
			{
				context.Employees.Remove(employee);	
				context.SaveChanges() ;
				return employee;
			}
			return employee;

		}

		public IEnumerable<Employee> GetAllEmployees()
		{
			return context.Employees;
		}

		public Employee GetEmployee(int id)
		{
			logger.LogInformation("info----------0");
			logger.LogCritical("critical----------0");
			logger.LogWarning("warning----------0");
			logger.LogTrace("trace----------0");
			return context.Employees.Find(id);
		}
		

		public Employee Update(Employee employeeChanges)
		{
			var e = context.Employees.Attach(employeeChanges);
			e.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			context.SaveChanges();
			return employeeChanges;
		}
	}
}
