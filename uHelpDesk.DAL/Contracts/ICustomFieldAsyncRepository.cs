using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.DAL.Repositories;
using uHelpDesk.Models;

namespace uHelpDesk.DAL.Contracts
{
    public interface ICustomFieldAsyncRepository : IAsyncRepository<CustomField>
    {
		Task<List<CustomField>> GetFieldsByEntityTypeAsync(string entityType);
	}
}
