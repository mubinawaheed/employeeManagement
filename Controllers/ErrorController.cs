using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace employeeManagement.Controllers
{
	public class ErrorController:Controller
	{
		[Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler (int statusCode)
		{
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature> ();
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Sorry, the resource you requested cannot be found";
					ViewBag.path = statusCodeResult?.OriginalPath;
					ViewBag.QS = statusCodeResult?.OriginalQueryString;
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
			ViewBag.ExceptionPath = exceptionDetails?.Path;
			ViewBag.ErrorDetails = exceptionDetails?.Error.Message;
			ViewBag.stackTrace = exceptionDetails?.Error.StackTrace;
			return View("~/Views/Shared/Error.cshtml");
		}
	}
}
