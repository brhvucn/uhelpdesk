using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Admin.Services.Contracts;
using uHelpDesk.DAL;

namespace uHelpDesk.Admin.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthService> _logger;
        private readonly uHelpDeskDbContext context;        

        public AuthService(UserManager<IdentityUser> userManager,
            uHelpDeskDbContext context,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.signInManager = signInManager;
            _logger = logger;
            this.context = context;           
        }

        /// <summary>
        /// When a new user is created the user registers itself using this method
        /// </summary>
        /// <param name="email">The email of the user, will be used as username</param>
        /// <param name="password">The password used to sign in</param>
        /// <param name="role">The internal role this user will have</param>
        /// <returns></returns>
        public async Task<CreateResult> RegisterUserAsync(string email, string password, string role = null)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new CreateResult()
                {
                    UserId = user.Id ?? email,
                    Result = result
                };
            }
            if (role != null && result.Succeeded)
            {
                await this.AddToRoleAsync(user.Id, role);
            }
            //if we got this far we may create additional objects for the user account
            
            return new CreateResult() { Result = result, UserId = user.Id };
        }

        public async Task<SignInResult> LoginUserAsync(string email, string password)
        {
            return await _userManager.CheckPasswordAsync(await _userManager.FindByEmailAsync(email), password)
                ? SignInResult.Success
                : SignInResult.Failed;
        }

        public async Task LogoutAsync()
        {
            // Implementation for logout
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveFromAllRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            IList<string> roles = await _userManager.GetRolesAsync(user);
            var result = IdentityResult.Success;
            foreach (var role in roles)
            {
                result = await _userManager.RemoveFromRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    break;
                }
            }
            return result;
        }

        public async Task<IdentityResult> UpdateRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            var res = await RemoveFromAllRolesAsync(userId);
            if (res.Succeeded)
            {
                res = await _userManager.AddToRoleAsync(user, newRole);
            }
            return res;
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<string>();
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync()
        {
            return await _roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task AddRole(string name)
        {
            bool exists = await _roleManager.RoleExistsAsync(name);
            if (!exists)
            {
                await _roleManager.CreateAsync(new IdentityRole(name));
            }
        }

        public async Task<IdentityResult> UpdateUser(IdentityUser user)
        {
            return await this._userManager.UpdateAsync(user);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await this._userManager.FindByEmailAsync(email);
        }

        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await this.signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
        }

        public Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }   
}