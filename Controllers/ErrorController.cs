using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace employeeManagement.Controllers
{
	public class ErrorController:Controller
	{
		private readonly ILogger<ErrorController> logger;

		public ErrorController(ILogger<ErrorController> logger)
        {
			this.logger = logger;
		}

		//This function is executed when a user tries to access a url that does not exist
        [Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler (int statusCode)
		{
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature> ();
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Sorry, the resource you requested cannot be found";
					logger.LogWarning($"{statusCode} error occured. Path = {statusCodeResult.OriginalPath}");
					break;

				case 500:
					ViewBag.ErrorMessage = "Internal server error";
					break;
			}
			return View("~/Views/Home/NotFound.cshtml");	
		}
		[Route("Error")]
		[AllowAnonymous]
		public IActionResult Error()
		{
			var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			logger.LogError($"{exceptionDetails?.Path} threw new error {exceptionDetails?.Error}");
			return View("~/Views/Shared/Error.cshtml");
		}
	}
}
