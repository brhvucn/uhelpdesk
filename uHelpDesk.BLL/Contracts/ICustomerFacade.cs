namespace uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;

public interface ICustomerFacade
{
    Task<IList<Customer>> GetAllCustomers();
    
}