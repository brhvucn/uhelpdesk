using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    public class TicketStatus : BaseModel
    {
        //empty constructor for EF
        public TicketStatus()
        {
        }
        public TicketStatus(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrEmpty();
            this.Name = name;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsClosedStatus { get; set; } //this property will be used to determine if a ticket is closed or not
        public int SortOrder { get; set; } //this property will be used to sort the status in the UI, also to determine the order of the status
    }
}
