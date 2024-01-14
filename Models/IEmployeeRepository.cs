﻿namespace employeeManagement.Models
{
    public interface IEmployeeRepository
    {
        //returns an object of type Employee
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployees(); 
        Employee AddEmployee(Employee employee);
    }
}
