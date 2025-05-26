using uHelpDesk.Models;

namespace uHelpDesk.BLL.Contracts;

public interface ICustomerFacade
{
    Task<IList<Customer>> GetAllCustomers();

    Task<Customer?> GetCustomerWithCustomFieldsByIdAsync(int id);

    Task<List<CustomField>> GetAllCustomFieldsAsync();

    Task SaveCustomFieldValuesAsync(int customerId, List<CustomFieldValue> values);
}