using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    /// <summary>
    /// This represents a ticket in the system.
    /// </summary>
    public class Ticket : BaseModel
    {
        public Ticket(string title, TicketStatus status)
        {
            Ensure.That(title, nameof(title)).IsNotNullOrEmpty();
            Ensure.That(status, nameof(status)).IsNotNull();
            this.Title = title;
            this.Status = status;
        }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; }
        public string? AssignedTo { get; set; }
        public IEnumerable<CustomField> CustomFields { get; set; } = new List<CustomField>(); //All the fields available for a Ticket
        public IEnumerable<CustomFieldValue> CustomValues { get; set; } = new List<CustomFieldValue>(); //All the values for the fields, may be less than the list
    }
}
