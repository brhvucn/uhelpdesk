using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using uHelpDesk.Admin.Services.Contracts;
using uHelpDesk.Admin.ViewModels.Account;

namespace uHelpDesk.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService authService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login(bool hasError = false)
        {
            var model = new LoginVM();

            if (hasError)
            {
                logger.LogError("Cannot log in with this combination of username and password");
                model.Error = "Cannot log in with this combination of username and password";
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DoLogin()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var result = await authService.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            logger.LogError("The user could not be logged in with this username and password combination.");
            return RedirectToAction("Login", new { hasError = true });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await authService.SignOutAsync();
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
                var result = await authService.RegisterUserAsync(model.Email, model.Password, model.Role);

                if (result.Result.Succeeded)
                {
                    logger.LogInformation("User created successfully.");
                    ShowSuccessMessage("User successfully created.");
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    logger.LogError(error.Description);
                }

                ShowFailMessage("User creation failed.");
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var users = await authService.GetUsers();
            var userVMs = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await authService.GetRolesAsync(user.Id);
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
            var user = await authService.GetUserById(id);
            if (user == null)
                return NotFound();

            var roles = await authService.GetRolesAsync(user.Id);

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

            var user = await authService.GetUserById(model.Id);
            if (user == null)
                return NotFound();

            user.Email = model.Email;

            var currentRoles = await authService.GetRolesAsync(user.Id);
            if (!currentRoles.Contains(model.Role))
            {
                foreach (var role in currentRoles)
                {
                    await authService.RemoveFromRoleAsync(user.Id, role);
                }

                await authService.AddToRoleAsync(user.Id, model.Role);
            }

            var result = await authService.UpdateUser(user);
            if (result.Succeeded)
            {
                ShowSuccessMessage("User updated successfully.");
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
            var result = await authService.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                ShowSuccessMessage("User deleted successfully.");
            }
            else
            {
                ShowFailMessage("Failed to delete user.");
            }

            return RedirectToAction("Index", "Account");
        }

        [AllowAnonymous]
        public async Task<string> SeedAdmin()
        {
            await authService.AddRole("Administrator");
            await authService.AddRole("Normal");
            await authService.RegisterUserAsync("admin@uhelpdesk.dk", "1q2w3e4r5t", "Administrator");
            logger.LogInformation("Created user and roles");
            return "Created user and roles";
        }
    }
}
