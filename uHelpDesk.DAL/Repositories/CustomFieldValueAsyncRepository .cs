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
    public class CustomFieldValueAsyncRepository : BaseRepository<CustomFieldValue>, ICustomFieldValueAsyncRepository
    {
        public CustomFieldValueAsyncRepository(uHelpDeskDbContext dbContext) : base(dbContext)
        {
        }
		public async Task<List<CustomFieldValue>> GetValuesForEntityAsync(int entityId)
		{
			return await context.CustomFieldValues
				.Where(cfv => cfv.EntityId == entityId)
				.Include(cfv => cfv.CustomField)
				.ToListAsync();
		}
	}
}
