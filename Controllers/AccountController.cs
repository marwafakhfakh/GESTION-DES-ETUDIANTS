using GESTIONDESETUDIANTS.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GESTIONDESETUDIANTS.Controllers
{

	public class AccountController : Controller
	{
        // GET: AccountController
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser>
        signInManager;
        public AccountController(UserManager<IdentityUser>
        userManager, SignInManager<IdentityUser> signInManager)
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
            if (ModelState.IsValid)
            {

                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser

                {
                    UserName = model.Email,
                    Email = model.Email
                };
                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)

                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,
                model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Logout()

        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
		{
			return View();
		}

		// GET: AccountController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: AccountController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AccountController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AccountController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AccountController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AccountController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AccountController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
