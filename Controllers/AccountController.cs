using Microsoft.AspNetCore.Mvc;
using employeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using employeeManagement.Models;

namespace employeeManagement.Controllers
{
	public class AccountController: Controller
	{
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, City=model.City };
                var result= await userManager.CreateAsync(user, password: model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false); //This bool param is used to define whether we want to create a session cookie or a permanent cookie
                    return RedirectToAction("index", "home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [AcceptVerbs("Post", "Get")]
		[AllowAnonymous]
		public async Task<IActionResult> CheckEmail(string email)
		{
           var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            return Json("Email is already in use");

		}
		[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl); //prone to open redirect attack
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                
                    ModelState.AddModelError("", "invalid credentials");
                
            }
            return View(model);
        }
    }
}
