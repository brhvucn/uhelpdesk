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
        //constructor for EF
        public Ticket() { }
        public Ticket(string title, TicketStatus status, Customer customer)
        {
            Ensure.That(title, nameof(title)).IsNotNullOrEmpty();
            Ensure.That(status, nameof(status)).IsNotNull();
            Ensure.That(customer, nameof(customer)).IsNotNull();
            this.Title = title;
            this.Status = status;
            this.Customer = customer;
        }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TicketStatusId { get; set; }
        public TicketStatus? Status { get; set; }
        public string? AssignedTo { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }        
        public IEnumerable<CustomFieldValue> CustomValues { get; set; } = new List<CustomFieldValue>(); //All the values for the fields, may be less than the list
    }
}
