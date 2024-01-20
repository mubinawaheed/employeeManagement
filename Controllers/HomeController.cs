﻿using employeeManagement.Models;
using employeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;


namespace employeeManagement.Controllers
{
	//[Route("Home")]
	//[Route("[controller]/[action]")] //Attribute routing aslo supports token replacement. controller is replaced with class name i.e. HOME and action is replaced with method name
	public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //Constructor injection
        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
			_webHostEnvironment = env;
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
        public IActionResult Create(EmployeeCreateViewModel model) //both viewResult and redirectToaction result implement this inteface IActionResult
        {
            if (ModelState.IsValid)
            {
                string? fileName = ProcessUploadedFile(model);
                Employee employee = new Employee{
                    Name = model.Name,
                    Email = model.Email,
                    Department= model.Department,
                    ProfileImage = fileName
                };
                _employeeRepository.AddEmployee(employee);
                 return RedirectToAction("details", new { id = employee.Id });
            }
            return View();

		}

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditView = new EmployeeEditViewModel
            {
                Id = id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.ProfileImage
            };
            return View(employeeEditView);
        }

        [HttpPost]
		public IActionResult Edit(EmployeeEditViewModel model) //both viewResult and redirectToaction result implement this inteface IActionResult
		{
			if (ModelState.IsValid)
			{
				Employee employee = _employeeRepository.GetEmployee(model.Id); //this id is coming from the hidden input field in edit view file
			    employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if(model.ProfileImage != null)
                {
				    employee.ProfileImage = ProcessUploadedFile(model);

                }
				_employeeRepository.Update(employee);
                if (model.ExistingPhotoPath!=null)
                {
                    string filepath=Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                    System.IO.File.Delete(filepath);

				}
				return RedirectToAction("index");
			}
			return View();

		}

		private string? ProcessUploadedFile(EmployeeCreateViewModel model)
		{
			string? fileName = null;
			if (model.ProfileImage != null && model.ProfileImage.Count > 0)
			{
				foreach (IFormFile photo in model.ProfileImage)
				{
					string folderName = Path.Combine(_webHostEnvironment.WebRootPath, "images");
					fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
					string combinedPath = Path.Combine(folderName, fileName);
                    using(var fileStream= new FileStream(combinedPath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
					

				}
			}

			return fileName;
		}
	}
}
