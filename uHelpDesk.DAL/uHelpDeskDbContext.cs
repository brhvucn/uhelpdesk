using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Models;

namespace uHelpDesk.DAL
{
    public class uHelpDeskDbContext : IdentityDbContext
    {
        public uHelpDeskDbContext(DbContextOptions<uHelpDeskDbContext> options)
            : base(options)
        {
        }

       public DbSet<Customer> Customers { get;set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<CustomFieldValue> CustomFieldValues { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
    }
}
