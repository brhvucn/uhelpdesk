using EnsureThat;
using uHelpDesk.Utilities.Extensions;

namespace uHelpDesk.Models
{
    /// <summary>
    /// A class representing a customer.
    /// </summary>
    public class Customer
    {
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
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
    }
}
