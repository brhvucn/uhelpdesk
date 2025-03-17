using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Admin.Models;
using uHelpDesk.Admin.Data;
using System.Collections;

namespace uHelpDesk.Admin.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly uHelpDeskContext _context;

        public TicketController(uHelpDeskContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/ticket/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
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


    }
}
