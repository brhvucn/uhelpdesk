using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace uHelpDesk.DAL.Repositories
{
    public class TicketRepository
    {
        private readonly uHelpDeskDbContext _context;

        public TicketRepository(uHelpDeskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.Include(t => t.Status).ToListAsync();
        }

    }
}
