using GESTIONDESETUDIANTS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GESTIONDESETUDIANTS.Controllers
{
	public class AdminController : Controller
	{
		private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]

		public IActionResult CreateRole()

		{

			return View();

		}
		[HttpPost]

		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)

		{
			if(ModelState.IsValid)

			{

				IdentityRole role = new IdentityRole { Name = model.RoleName };
				IdentityResult result = await roleManager.CreateAsync(role);

				if

				(result.Succeeded
				)

				{

					return RedirectToAction("Index", "Home");



                }
				foreach

				(IdentityError error in result.Errors
				)

				{

					ModelState.AddModelError

					(string.Empty, error.Description);

				}
			}

			return View(model);

		}
		[HttpGet]
		public IActionResult ListRoles()
		{
			var roles = roleManager.Roles;
			return View(roles);
		}
        // Role ID is passed from the URL to the action

        [HttpGet ]

        public async Task<IActionResult> EditRole(string id)



        {
            // Find the role by Role ID

            var role = await roleManager.FindByIdAsync(id);
            if

            (role == null)

            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                

                return View("NotFound");


            }

            var model = new EditRoleViewModel

            {

                Id = role.Id,RoleName = role.Name
            };
            // Retrieve all the Users
            foreach (var user in userManager.Users.ToList())

            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display

                if

                (await userManager.IsInRoleAsync(user, role.Name))
                {

                    model.Users.Add (user.UserName);

                }
            }

            return View(model);

        }
        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";

                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync

                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)

                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)

        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {

                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";

                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)

                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }
        }
    }


	// Reste du code }
}
