using Microsoft.EntityFrameworkCore;
using uHelpDesk.Admin.Models;
using uHelpDesk.DAL;

namespace uHelpDesk.Admin.Data
{
    public class uHelpDeskContext : DbContext
    {
        public uHelpDeskContext(DbContextOptions<uHelpDeskContext> options) : base(options) {}

        public DbSet<Ticket> Tickets { get; set; }

    }
}
