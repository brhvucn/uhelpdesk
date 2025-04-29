using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;
using uHelpDesk.DAL.Contracts;

namespace uHelpDesk.BLL
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly ICustomerAsyncRepository _customerRepository;

        public CustomerFacade(ICustomerAsyncRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<IList<Customer>> GetAllCustomers()
        {
            return await this._customerRepository.GetAllAsync();
        }
    }
}
