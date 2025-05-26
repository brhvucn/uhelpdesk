using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.Models;

namespace uHelpDesk.BLL.Contracts
{
    public interface ITicketFacade
    {
        Task<Ticket> CreateTicketAsync(string title, int customerId, int statusId, string description = "");
        Task<bool> AssignCustomerToTicketAsync(int ticketId, int customerId);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
    }
}
