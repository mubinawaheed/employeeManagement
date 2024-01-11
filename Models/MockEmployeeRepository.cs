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
                new Employee() {Id=1, Name="Mubina", Department="CS", Email="mubina@gmail.com"},
                new Employee() {Id=2, Name="Samra", Department="CS", Email="samra@gmail.com"},
				new Employee() {Id=3, Name="Abdullah", Department="IVS", Email="abdullah@gmail.com"},

			    new Employee() {Id=4, Name="Ali", Department="CS", Email="ali@gmail.com"}
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
