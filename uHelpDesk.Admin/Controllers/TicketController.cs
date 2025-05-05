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
    [Route("Tickets")]
    [Authorize]
    public class TicketController : BaseController
    {

        private readonly TicketService _ticketService;

        private readonly uHelpDeskDbContext _context;

        public TicketController(TicketService ticketService, uHelpDeskDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }

        // GET: /Tickets
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.Customer)
                .ToListAsync();

            return View(tickets);
        }

        // GET: Tickets/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.StatusList = new SelectList(_context.TicketStatuses.OrderBy(s => s.SortOrder), "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }
    }
}
