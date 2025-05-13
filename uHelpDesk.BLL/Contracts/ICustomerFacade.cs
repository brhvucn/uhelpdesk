using uHelpDesk.Models;

namespace uHelpDesk.BLL.Contracts
{
    public interface ICustomerFacade
    {
        Task<IList<Customer>> GetAllCustomers();

        Task<Customer?> GetCustomerById(int id);

        Task<bool> CreateCustomer(Customer customer);

        Task<bool> UpdateCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);
    }
}