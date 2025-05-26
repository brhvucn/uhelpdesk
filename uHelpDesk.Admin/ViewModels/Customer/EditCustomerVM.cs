using CustomFieldModel = uHelpDesk.Models.CustomField;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class EditCustomerVM : CreateCustomerVM
    {
        public int Id { get; set; }

        // For assigning a new custom field
        public int SelectedCustomFieldId { get; set; } // dropdown selection
        public string CustomFieldValue { get; set; } = "";

        public List<CustomFieldModel> AvailableFields { get; set; } = new(); // dropdown options
    }
}