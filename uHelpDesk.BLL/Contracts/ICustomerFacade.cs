namespace uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;

public interface ICustomerFacade
{
    Task<IList<Customer>> GetAllCustomers();

    Task<Customer?> GetCustomerWithCustomFieldsByIdAsync(int id);
    Task<List<CustomField>> GetAllCustomFieldsAsync();
    Task SaveCustomFieldValueAsync(int customerId, List<CustomFieldEntry> fields);
}