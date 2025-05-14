using System;

namespace uHelpDesk.Admin.ViewModels.Ticket
{
    public class TicketDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}