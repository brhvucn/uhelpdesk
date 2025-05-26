using Microsoft.EntityFrameworkCore;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uHelpDesk.DAL.Repositories
{
    public class CustomFieldAsyncRepository : BaseRepository<CustomField>, ICustomFieldAsyncRepository
    {
        public CustomFieldAsyncRepository(uHelpDeskDbContext dbContext) : base(dbContext) { }

        public async Task<IList<CustomField>> GetByEntityTypeAsync(string entityType)
        {
            return await context.CustomFields
                .Where(f => f.EntityType == entityType)
                .ToListAsync();
        }

        public async Task<IList<CustomFieldValue>> GetValuesForCustomerAsync(int customerId)
        {
            return await context.CustomFieldValues
                .Include(v => v.CustomField)
                .Where(v => v.EntityId == customerId)
                .ToListAsync();
        }

        public async Task SaveValuesForCustomerAsync(int customerId, List<CustomFieldValue> values)
        {
            var existingValues = await context.CustomFieldValues
                .Where(v => v.EntityId == customerId)
                .ToListAsync();

            foreach (var newValue in values)
            {
                var existing = existingValues
                    .FirstOrDefault(ev => ev.CustomFieldId == newValue.CustomFieldId);

                if (existing != null)
                {
                    // Update existing value
                    existing.Value = newValue.Value;
                    context.CustomFieldValues.Update(existing);
                }
                else
                {
                    // Set the EntityId and add new custom field value
                    newValue.EntityId = customerId;
                    context.CustomFieldValues.Add(newValue);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}