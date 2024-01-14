using employeeManagement.Models;
using employeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;


namespace employeeManagement.Controllers
{
	//[Route("Home")]
	//[Route("[controller]/[action]")] //Attribute routing aslo supports token replacement. controller is replaced with class name i.e. HOME and action is replaced with method name
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
		//[Route("details/{id?}")] //question mark makes the id param optional
		//[Route("{id?}")]
		public ViewResult Details(int? id)
        {
            //we can also return the data as ObjectResult type. Also, If we are building API, we'll return modeldata as json type, else we return a view type result.
            Employee model = _employeeRepository.GetEmployee(id??1); 

            //View model approach
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "Employee Details"
            };
            return View(viewModel); 

        }

        [HttpPost]
        public IActionResult Create(Employee employee) //both viewResult and redirectToaction result implement this inteface IActionResult
        {
            if (ModelState.IsValid)
            {
                Employee newUser = _employeeRepository.AddEmployee(employee);
               // return RedirectToAction("details", new { id = newUser.Id });
            }
            return View();

		}

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
    }
}
