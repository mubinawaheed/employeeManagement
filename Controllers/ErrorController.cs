using Microsoft.AspNetCore.Mvc;

namespace employeeManagement.Controllers
{
	public class ErrorController:Controller
	{
		[Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler (int statusCode)
		{
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Sorry, the resource you requested cannot be found";
					break;

				case 500:
					ViewBag.ErrorMessage = "Internal server error";
					break;
			}
			return View("NotFound");	
		}
	}
}
