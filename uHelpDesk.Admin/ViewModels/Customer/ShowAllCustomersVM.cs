namespace uHelpDesk.Admin.ViewModels.Customer;

using uHelpDesk.Admin.Utils;
using uHelpDesk.Models;

public class ShowAllCustomersVM : BaseVM
{
    public ShowAllCustomersVM()
    {
        this.PageTitle = Names.CustomerNames.AllCustomers;
    }
    public IList<Customer> Customers = new List<Customer>();
}