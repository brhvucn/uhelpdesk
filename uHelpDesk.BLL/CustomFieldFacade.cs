using uHelpDesk.BLL.Contracts;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.BLL
{
    public class CustomFieldFacade : ICustomFieldFacade
    {
        private readonly ICustomFieldAsyncRepository _repository;

        public CustomFieldFacade(ICustomFieldAsyncRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<CustomField>> GetCustomFieldsForEntityAsync(string entityType)
        {
            return await _repository.GetByEntityTypeAsync(entityType);
        }

        public async Task<CustomField?> GetCustomFieldByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> CreateCustomFieldAsync(CustomField field)
        {
            try
            {
                await _repository.AddAsync(field);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCustomFieldAsync(CustomField field)
        {
            try
            {
                await _repository.UpdateAsync(field);
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DeleteCustomFieldAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch { return false; }
        }
    }
}