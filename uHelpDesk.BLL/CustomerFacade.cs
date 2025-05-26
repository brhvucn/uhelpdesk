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

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<bool> CreateCustomer(Customer customer)
    {
        try
        {
            await _customerRepository.AddAsync(customer);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        try
        {
            await _customerRepository.UpdateAsync(customer);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        try
        {
            await _customerRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}