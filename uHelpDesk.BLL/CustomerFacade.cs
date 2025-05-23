using Microsoft.EntityFrameworkCore;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.BLL.DTOS;
using uHelpDesk.DAL;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.BLL
{
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

        public async Task SaveCustomFieldValuesAsync(int customerId, List<CustomFieldDTO> entries)
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
