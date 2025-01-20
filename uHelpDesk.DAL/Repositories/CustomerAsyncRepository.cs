using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.DAL.Repositories
{
    public class CustomerAsyncRepository : BaseRepository<Customer>, ICustomerAsyncRepository
    {
        public CustomerAsyncRepository(uHelpDeskDbContext dbContext) : base(dbContext)
        {
        }
    }
}
