using uHelpDesk.BLL.Contracts;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

public class CustomerFacade : ICustomerFacade
{
    private readonly ICustomerAsyncRepository _customerRepository;

    public CustomerFacade(ICustomerAsyncRepository customerRepository)
    {
        this._customerRepository = customerRepository;
    }

    public async Task<IList<Customer>> GetAllCustomers()
    {
        return await _customerRepository.GetAllAsync();
    }

    public Task<Customer> GetCustomerById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateCustomer(Customer customer)
    {
        await _customerRepository.AddAsync(customer);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomer(int id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}