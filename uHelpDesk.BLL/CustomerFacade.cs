using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.BLL.DTOS;

namespace uHelpDesk.BLL
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly ICustomerAsyncRepository _customerRepository;
        private readonly ICustomFieldAsyncRepository _customFieldRepository;

        public CustomerFacade(ICustomerAsyncRepository customerRepository, ICustomFieldAsyncRepository customFieldRepository)
        {
            this._customerRepository = customerRepository;
            this._customFieldRepository = customFieldRepository;
        }
        public async Task<IList<Customer>> GetAllCustomers()
        {
            return await this._customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerWithCustomFieldsByAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            // Optional: fetch custom fields and map them
            return customer;
        }

        public async Task<List<CustomField>> GetAllCustomFieldsAsync()
        {
            return (await _customFieldRepository.GetByEntityTypeAsync("Customer")).ToList();
        }

        public async Task SaveCustomFieldValuesAsync(int customerId, List<CustomFieldEntry> entries)
        {
            var values = entries.Select(entry => new CustomFieldValue
            {
                EntityId = customerId,
                CustomFieldId = entry.CustomFieldId,
                Value = entry.Value
            }).ToList();

            await _customFieldRepository.SaveValuesForCustomerAsync(customerId, values);
        }
    }
}
