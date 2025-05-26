using EnsureThat;
using uHelpDesk.Utilities.Extensions;

namespace uHelpDesk.Models
{
    /// <summary>
    /// A class representing a customer.
    /// </summary>
    public class Customer : BaseModel
    {
        //empty constructor for EF
        public Customer() { }
        /// <summary>
        /// A customer must be created with a name and an email.
        /// </summary>
        /// <param name="name">The name of the customer</param>
        /// <param name="email">The email of the customer</param>
        public Customer(string name, string email)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrEmpty();
            Ensure.That(email, nameof(email)).IsNotNullOrEmpty();
            Ensure.That(email, nameof(email)).IsEmail();
            this.Name = name;
            this.Email = email;
        }
        //common properties for a customer
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; } = new List<Ticket>(); //All the tickets for a customer        
        public IEnumerable<CustomFieldValue> CustomValues { get; set; } = new List<CustomFieldValue>(); //All the values for the fields, may be less than the list
    }
}
