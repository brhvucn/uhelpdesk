using Microsoft.EntityFrameworkCore;
using uHelpDesk.Admin.Models;
using uHelpDesk.DAL;
using uHelpDesk.Models;

namespace uHelpDesk.Admin.Data
{
    public class uHelpDeskContext : DbContext
    {
        public uHelpDeskContext(DbContextOptions<uHelpDeskDbContext> options) : base(options) {}

        public DbSet<Ticket> Tickets { get; set; }

    }
}
