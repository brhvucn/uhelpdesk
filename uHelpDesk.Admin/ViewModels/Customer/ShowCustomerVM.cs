using System.Security.Permissions;
using CustomerModel = uHelpDesk.Models.Customer;
using CustomFieldModel = uHelpDesk.Models.CustomField;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class ShowCustomerVM
    {
        // Basic customer info
        public int Id => Customer?.Id ?? 0;
        public string Name => Customer?.Name ?? string.Empty;
        public string Email => Customer?.Email ?? string.Empty;

        // Full customer object
        public CustomerModel Customer { get; set; }

        // Custom fields display
        public Dictionary<string, string> CustomFields { get; set; } = new();

        // For assigning a new custom field
        public int SelectedCustomFieldId { get; set; }
        public string CustomFieldValue { get; set; } = "";

        public List<CustomFieldModel> AvailableFields { get; set; } = new();
    }
}