using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Models;
using uHelpDesk.DAL;
using uHelpDesk.Admin.Services;
using uHelpDesk.Admin.ViewModels.Ticket;
using System.Threading.Tasks;
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
            ViewBag.CustomerList = new SelectList(_context.Customers.OrderBy(c => c.Id), "Id", "Id");
            return View();
        }

        // POST: Tickets/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    Title = model.Title,
                    Description = model.Description,
                    TicketStatusId = model.StatusId,
                    CustomerId = model.CustomerId
                };

                _context.Add(ticket);
                await _context.SaveChangesAsync();

                TempData["success"] = "Ticket created successfully!";
                return RedirectToAction(nameof(Index));
            }

            model.StatusList = new SelectList(await _context.TicketStatuses.ToListAsync(), "Id", "Name");
            model.CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "Name");
            return View(model);
        }

        // GET: Tickets/Edit/{id}
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var model = new TicketEditViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                TicketStatusId = ticket.TicketStatusId,
                CustomerId = ticket.CustomerId,
                StatusList = new SelectList(await _context.TicketStatuses.ToListAsync(), "Id", "Name", ticket.TicketStatusId),
                CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "Name", ticket.CustomerId)
            };

            return View(model);
        }

        // POST: Tickets/Edit/{id}
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }

                // Update ticket properties
                ticket.Title = model.Title;
                ticket.Description = model.Description;
                ticket.TicketStatusId = model.TicketStatusId;
                ticket.CustomerId = model.CustomerId;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Add a success message
                TempData["success"] = "Ticket updated successfully!";

                // Redirect to the Index page after editing
                return RedirectToAction(nameof(Index));
            }

            // If model is invalid, reload the lists and return to the Edit view
            model.StatusList = new SelectList(await _context.TicketStatuses.ToListAsync(), "Id", "Name", model.TicketStatusId);
            model.CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "Name", model.CustomerId);

            return View(model);
        }

        // GET: Tickets/Delete/{id}
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/{id}
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();

                TempData["success"] = "Ticket deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Details/{id}
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }
            var model = new TicketDetailsViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CustomerName = ticket.Customer?.Name, 
                StatusName = ticket.Status?.Name,      
                AssignedTo = ticket.AssignedTo?.ToString("g"),  
                ResolvedAt = ticket.ResolvedAt
            };

            return View(model);
        }
    }
}
