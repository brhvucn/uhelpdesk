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
    }
}