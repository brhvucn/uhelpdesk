using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using uHelpDesk.Admin.Services.Contracts;
using uHelpDesk.Admin.ViewModels.Account;

namespace uHelpDesk.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        public IActionResult Login(bool hasError = false)
        {
            LoginVM model = new LoginVM();
            if (hasError)
            {
                this.logger.LogError("Cannot log in with this combination of username and password");
                model.Error = "Cannot log in with this combination of username and password";
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> DoLogin()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            var result = await this.authService.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            this.logger.LogError("The user could not be logged in with this username and password combination.");
            return RedirectToAction("Login", new { hasError = true });
        }

        public async Task<IActionResult> Logout()
        {
            await this.authService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegisterUser(RegisterUserVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.authService.RegisterUserAsync(model.Email, model.Password, model.Role);
                if (result.Result.Succeeded)
                {
                    this.logger.LogInformation("User created successfully.");
                    TempData["success"] = "User successfully created.";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    this.logger.LogError(error.Description);
                }

                TempData["error"] = "User creation failed.";
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var users = await this.authService.GetUsers();
            var userVMs = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await this.authService.GetRolesAsync(user.Id);
                userVMs.Add(new UserVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Unassigned"
                });
            }

            return View(userVMs);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.authService.GetUserById(id);
            if (user == null) return NotFound();

            var roles = await this.authService.GetRolesAsync(user.Id);

            var model = new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(UserVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await this.authService.GetUserById(model.Id);
            if (user == null)
                return NotFound();

            user.Email = model.Email;

            var currentRoles = await this.authService.GetRolesAsync(user.Id);

            if (!currentRoles.Contains(model.Role))
            {
                foreach (var role in currentRoles)
                {
                    await this.authService.RemoveFromRoleAsync(user.Id, role);
                }

                await this.authService.AddToRoleAsync(user.Id, model.Role);
            }

            var result = await this.authService.UpdateUser(user);
            if (result.Succeeded)
            {
                TempData["success"] = "User updated successfully.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await this.authService.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                TempData["success"] = "User deleted successfully.";
            }
            else
            {
                TempData["error"] = "Failed to delete user.";
            }

            return RedirectToAction("Index");
        }

        public async Task<string> SeedAdmin()
        {
            await this.authService.AddRole("Administrator");
            await this.authService.AddRole("Normal");
            await this.authService.RegisterUserAsync("admin@uhelpdesk.dk", "1q2w3e4r5t", "Administrator");
            this.logger.LogInformation("Created user and roles");
            return "Created user and roles";
        }
    }
}
