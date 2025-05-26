using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uHelpDesk.Models;
using uHelpDesk.DAL;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.Admin.ViewModels.Ticket;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace uHelpDesk.Admin.Controllers
{
    [Route("Ticket")]
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicketFacade _ticketService;
        private readonly uHelpDeskDbContext _context;

        public TicketController(ITicketFacade ticketService, uHelpDeskDbContext context)
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
        public async Task<IActionResult> Create()
        {
            var model = new TicketCreateViewModel
            {
                StatusList = new SelectList(_context.TicketStatuses.OrderBy(s => s.SortOrder), "Id", "Name"),
                CustomerList = new SelectList(_context.Customers.OrderBy(c => c.Name), "Id", "Name"),
            };
            return View(model);

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

                await _context.SaveChangesAsync();
                ShowSuccessMessage("Ticket created successfully!");  // Show success message
                return RedirectToAction(nameof(Index));
            }

            model.StatusList = new SelectList(await _context.TicketStatuses.ToListAsync(), "Id", "Name");
            model.CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "Name");
            ShowFailMessage("Please correct the errors in the form.");  // Show failure message
            return View(model);
        }
    }
}

