namespace uHelpDesk.BLL.Contracts
{
    using uHelpDesk.Models;

    public interface ICustomerFacade
    {
        // Get all customers
        Task<IList<Customer>> GetAllCustomers();
        
        // Get a customer by ID
        Task<Customer> GetCustomerById(int id);

        // Create a new customer
        Task CreateCustomer(Customer customer);

        // Update an existing customer
        Task UpdateCustomer(Customer customer);

        // Delete a customer
        Task DeleteCustomer(int id);
    }
}