using System.Security.Permissions;
using CustomerModel = uHelpDesk.Models.Customer;
using CustomFieldModel = uHelpDesk.Models.CustomField;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class ShowCustomerVM
    {
        public CustomerModel Customer { get; set; }
        public Dictionary<string, string> CustomFields { get; set; } = new();

        // For assigning a new custom field
        public int SelectedCustomFieldId { get; set; } // dropdown selection
        public string CustomFieldValue { get; set; } = "";

        public List<CustomFieldModel> AvailableFields { get; set; } = new(); // dropdown options
    }
}
