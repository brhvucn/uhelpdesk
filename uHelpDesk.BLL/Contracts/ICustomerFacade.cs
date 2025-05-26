using uHelpDesk.Models;

namespace uHelpDesk.BLL.Contracts;

public interface ICustomerFacade
{
    Task<IList<Customer>> GetAllCustomers();

    Task<Customer?> GetCustomerById(int id);

    Task<bool> CreateCustomer(Customer customer);

    Task<bool> UpdateCustomer(Customer customer);

    Task<bool> DeleteCustomer(int id);

    Task<Customer?> GetCustomerWithCustomFieldsByIdAsync(int id);

    Task<List<CustomField>> GetAllCustomFieldsAsync();

    Task SaveCustomFieldValuesAsync(int customerId, List<CustomFieldValue> values);
}