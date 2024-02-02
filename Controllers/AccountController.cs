using Microsoft.AspNetCore.Mvc;
using employeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace employeeManagement.Controllers
{
	public class AccountController: Controller
	{
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
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
    }
}
