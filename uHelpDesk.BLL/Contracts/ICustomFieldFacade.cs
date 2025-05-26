using uHelpDesk.Models;

namespace uHelpDesk.BLL.Contracts
{
    public interface ICustomFieldFacade
    {
        Task<IList<CustomField>> GetCustomFieldsForEntityAsync(string entityType);
        Task<CustomField?> GetCustomFieldByIdAsync(int id);
        Task<bool> CreateCustomFieldAsync(CustomField field);
        Task<bool> UpdateCustomFieldAsync(CustomField field);
        Task<bool> DeleteCustomFieldAsync(int id);
    }
}
    
