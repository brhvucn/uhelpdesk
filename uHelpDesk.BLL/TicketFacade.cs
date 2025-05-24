using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uHelpDesk.DAL;
using uHelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.DAL.Migrations;
using uHelpDesk.BLL.Contracts;

namespace uHelpDesk.Admin.Services
{
    public class TicketFacade : ITicketFacade
    {
        private readonly uHelpDeskDbContext _context;

        public TicketFacade(uHelpDeskDbContext context)
        {
            _context = context;
        }

        // Create a ticket
        public async Task<Ticket> CreateTicketAsync(string title, int customerId, int statusId, string description = "")
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var ticket = new Ticket
            {
                Title = title,
                Description = description,
                CustomerId = customerId,
                TicketStatusId = statusId,
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;

        }

        // Assign a customer to a ticket
        public async Task<bool> AssignCustomerToTicketAsync(int ticketId, int customerId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            var customer = await _context.Customers.FindAsync(customerId);

            if (ticket == null || customer == null)
            {
                return false;
            }

            ticket.CustomerId = customerId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Get all tickets
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Status)
                .ToListAsync();
        }

        // Get a ticket by ID
        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }
    }
}
