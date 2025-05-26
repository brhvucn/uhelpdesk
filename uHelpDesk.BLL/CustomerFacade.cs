using Microsoft.EntityFrameworkCore;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.DAL;
using uHelpDesk.Models;

namespace uHelpDesk.BLL;

public class CustomerFacade : ICustomerFacade
{
    private readonly ICustomerAsyncRepository _customerRepository;
    private readonly ICustomFieldAsyncRepository _customFieldRepository;
    private readonly uHelpDeskDbContext _context;

    public CustomerFacade(ICustomerAsyncRepository customerRepository, ICustomFieldAsyncRepository customFieldRepository, uHelpDeskDbContext context)
    {
        this._customerRepository = customerRepository;
        this._customFieldRepository = customFieldRepository;
        this._context = context;
    }

    public async Task<IList<Customer>> GetAllCustomers()
    {
        return await this._customerRepository.GetAllAsync();
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

    public async Task<Customer?> GetCustomerWithCustomFieldsByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.CustomValues)
            .ThenInclude(v => v.CustomField)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<CustomField>> GetAllCustomFieldsAsync()
    {
        return (await _customFieldRepository.GetByEntityTypeAsync("Customer")).ToList();
    }

    public async Task SaveCustomFieldValuesAsync(int customerId, List<CustomFieldValue> values)
    {
        // Ensure EntityId is set correctly on each value
        foreach (var val in values)
        {
            val.EntityId = customerId;
        }

        await _customFieldRepository.SaveValuesForCustomerAsync(customerId, values);
    }
}