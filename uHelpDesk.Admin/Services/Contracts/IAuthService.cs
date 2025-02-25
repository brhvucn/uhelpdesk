using Microsoft.AspNetCore.Identity;

namespace uHelpDesk.Admin.Services.Contracts
{
    public interface IAuthService
    {
        Task<CreateResult> RegisterUserAsync(string email, string password, string role = null);
        Task<SignInResult> LoginUserAsync(string email, string password);
        Task LogoutAsync();
        Task<IdentityResult> ResetPasswordAsync(string email, string newPassword);
        Task SignOutAsync();

        // Reading user information
        Task<IdentityUser> GetUserByEmail(string email);
        Task<IdentityUser> GetUserById(string id);
        Task<List<IdentityUser>> GetUsers(); // Return a collection of users

        // Managing roles
        Task<IdentityResult> AddToRoleAsync(string userId, string role);
        Task<IdentityResult> RemoveFromRoleAsync(string userId, string role);
        Task<IList<string>> GetRolesAsync(string userId);
        Task<IList<string>> GetRolesAsync();
        Task AddRole(string name);
        Task<IdentityResult> UpdateRoleAsync(string userid, string role);

        // Managing users
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> UpdateUser(IdentityUser user);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(IdentityUser user);
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string newPassword);
    }
}
