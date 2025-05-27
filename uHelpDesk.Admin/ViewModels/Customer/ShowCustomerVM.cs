using CustomerModel = uHelpDesk.Models.Customer;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class ShowCustomerVM
    {
        public CustomerModel Customer { get; set; }
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}