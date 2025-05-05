using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Models;
using Microsoft.Extensions.Configuration;

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
        public DbSet<TicketAnswer> TicketAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //map relationships
            builder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId);
            //cannot map Custom fields since they are related to multiple entities

            builder.Entity<Customer>()
                .HasMany<Ticket>(x=>x.Tickets)                
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.Entity<Ticket>()
                .HasOne<TicketStatus>()
                .WithMany()
                .HasForeignKey(x => x.TicketStatusId);

            //configure relationship between ticket and a list of ticket answers
            builder.Entity<Ticket>()
                .HasMany<TicketAnswer>(x => x.TicketAnswers)
                .WithOne(x => x.Ticket)
                .HasForeignKey(x => x.TicketId);

            builder.Entity<Ticket>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.TicketStatusId);
        }
    }
}
