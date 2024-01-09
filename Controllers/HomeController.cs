using employeeManagement.Models;
using employeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;


namespace employeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        //Constructor injection
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }
        public ViewResult Details()
        {
            //we can also return the data as ObjectResult type. Also, If we are building API, we'll return modeldata as json type, else we return a view type result.
            Employee model = _employeeRepository.GetEmployee(1);

            //View model approach
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "Employee Details"
            };
            return View(viewModel); 

        }
    }
}
