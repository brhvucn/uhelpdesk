using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    public class TicketAnswer : BaseModel
    {        
        public TicketAnswer() { } //for EF
        public TicketAnswer(string description, string createdBy, bool isInternal = true)
        {
            Ensure.That(description, nameof(description)).IsNotNullOrEmpty();
            Ensure.That(createdBy, nameof(createdBy)).IsNotNullOrEmpty();

            Description = description;
            CreatedBy = createdBy;
            IsInternal = isInternal;
            Created = DateTime.UtcNow;
        }
        public string Description { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } //the id of the user who created this answer
        public bool IsInternal { get; set; } //determines if this is an answer by a supporter or by a customer. If it is internal it is "us", a supporter that answers it. This determines the dialog structure.
    }
}
