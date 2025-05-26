using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc.Rendering;
using uHelpDesk.Models;

namespace uHelpDesk.Admin.ViewModels.Ticket
{
    public class TicketCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int StatusId { get; set; }
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}
