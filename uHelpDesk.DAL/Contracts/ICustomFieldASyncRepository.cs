using uHelpDesk.Models;

namespace uHelpDesk.DAL.Contracts
{
    public interface ICustomFieldAsyncRepository : IAsyncRepository<CustomField>
    {
        Task<IList<CustomField>> GetByEntityTypeAsync(string entityType);
    }
}