using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace uHelpDesk.Admin.ViewModels.Ticket
{
    public class TicketEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int CustomerId { get; set; }
        public int TicketStatusId { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}