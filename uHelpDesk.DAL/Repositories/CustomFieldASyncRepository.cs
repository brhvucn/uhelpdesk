using Microsoft.EntityFrameworkCore;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

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
            var existing = await context.CustomFieldValues
                .Where(v => v.EntityId == customerId)
                .ToListAsync();

            context.CustomFieldValues.RemoveRange(existing); // delete old values
            await context.CustomFieldValues.AddRangeAsync(values); // add new ones

            await context.SaveChangesAsync();
        }
    }
}