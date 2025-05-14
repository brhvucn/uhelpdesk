using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.DAL.Repositories
{
    public class CustomFieldAsyncRepository : BaseRepository<CustomField>, ICustomFieldAsyncRepository
    {
        public CustomFieldAsyncRepository(uHelpDeskDbContext dbContext) : base(dbContext)
        {
        }
		public async Task<List<CustomField>> GetFieldsByEntityTypeAsync(string entityType)
		{
			return await context.CustomFields
				.Where(cf => cf.EntityType == entityType && cf.IsActive)
				.ToListAsync();
		}
	}
}

