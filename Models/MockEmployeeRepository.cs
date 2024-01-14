namespace employeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        //This constructor will initialize this private field with some mock data
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id=1, Name="Mubina", Department=Dept.HR, Email="mubina@gmail.com"},
                new Employee() {Id=2, Name="Samra", Department=Dept.IT, Email="samra@gmail.com"},
				new Employee() {Id=3, Name="Abdullah", Department=Dept.CS, Email="abdullah@gmail.com"},

			    new Employee() {Id=4, Name="Ali", Department=Dept.IT, Email="ali@gmail.com"}
            };
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException($"Employee with ID {id} not found");
            // ?? this is null-coalescing  operator
        }

        public IEnumerable<Employee> GetAllEmployees() {
            return _employeeList;
        } 
    }
}
