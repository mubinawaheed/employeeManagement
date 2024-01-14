namespace employeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; } //adding ? to indicate it can be null
        public Dept? Department { get; set; }
        public string? Email { get; set; }


    }
}
