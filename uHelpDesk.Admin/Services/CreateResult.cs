using Microsoft.AspNetCore.Identity;

namespace uHelpDesk.Admin.Services
{
    public class CreateResult
    {
        public IdentityResult Result { get; set; }
        public string UserId { get; set; }
    }
}
