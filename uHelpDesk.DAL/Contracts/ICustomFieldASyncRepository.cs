using uHelpDesk.Models;

namespace uHelpDesk.DAL.Contracts
{
    public interface ICustomFieldAsyncRepository : IAsyncRepository<CustomField>
    {
        Task<IList<CustomField>> GetByEntityTypeAsync(string entityType);
        Task<IList<CustomFieldValue>> GetValuesForCustomerAsync(int customerId);
        Task SaveValuesForCustomerAsync(int customerId, List<CustomFieldValue> values);
    }
}