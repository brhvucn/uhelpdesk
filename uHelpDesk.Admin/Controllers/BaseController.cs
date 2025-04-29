using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace uHelpDesk.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected void ShowSuccessMessage(string message)
        {
            TempData["success"] = message;
        }

        protected void ShowFailMessage(string message)
        {
            TempData["error"] = message;
        }
    }
}