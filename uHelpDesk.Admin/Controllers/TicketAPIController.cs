using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Models;
using uHelpDesk.DAL;
using uHelpDesk.Admin.Services;
using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace uHelpDesk.Admin.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class TicketAPIController : ControllerBase
    {
        private readonly TicketService _ticketService;

        private readonly uHelpDeskDbContext _context;

        public TicketAPIController(TicketService ticketService, uHelpDeskDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            var createdTicket = await _ticketService.CreateTicketAsync(ticket.Title, ticket.CustomerId, ticket.TicketStatusId, ticket.Description);
            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.Id }, createdTicket);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Calculate time spent if assigned
            if (ticket.AssignedTo.HasValue && ticket.ResolvedAt.HasValue)
            {
                ticket.TimeSpent = ticket.ResolvedAt.Value - ticket.AssignedTo.Value;
            }
            else if (ticket.AssignedTo.HasValue)
            {
                ticket.TimeSpent = DateTime.UtcNow - ticket.AssignedTo.Value;
            }

            await _context.SaveChangesAsync(); // Save to the database

            return Ok(ticket);
        }

        [HttpPost("{ticketId}/assign/{customerId}")]
        public async Task<IActionResult> AssignCustomer(int ticketId, int customerId)
        {
            var result = await _ticketService.AssignCustomerToTicketAsync(ticketId, customerId);
            if (!result)
            {
                return BadRequest("Could not assign customer to ticket");
            }
            return Ok("Customer assigned successfully");
        }


        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return Ok(await _context.Tickets.ToListAsync());
        }

        // PUT: api/ticket/id
        [HttpPut]
        public async Task<IActionResult> UpdateTicket(int id, Ticket updatedTicket)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Title = updatedTicket.Title;
            ticket.Description = updatedTicket.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ticket/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Update timestamps for tickets
        [HttpPost("assign/{ticketId}")]
        public async Task<IActionResult> AssignTicket(int ticketId, string agentId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null) return NotFound();

            ticket.AssignedTo = DateTime.UtcNow; // Sets the assignment for time
            ticket.TimeSpent = DateTime.UtcNow - ticket.CreatedAt; // Calculates time spent so far

            await _context.SaveChangesAsync();
            return Ok(ticket);

        }
    }
}
