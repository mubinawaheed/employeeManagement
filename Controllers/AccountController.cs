using Microsoft.AspNetCore.Mvc;

namespace employeeManagement.Controllers
{
	public class AccountController: Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
	}
}
