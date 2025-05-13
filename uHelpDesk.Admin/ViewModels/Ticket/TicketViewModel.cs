using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace uHelpDesk.Admin.ViewModels.Ticket
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int StatusId { get; set; }
        public int CustomerId { get; set; }

        // Additional properties for dropdowns
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}