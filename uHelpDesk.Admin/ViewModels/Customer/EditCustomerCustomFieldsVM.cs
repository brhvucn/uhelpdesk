using System.Collections.Generic;
using uHelpDesk.Models;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class EditCustomerCustomFieldsVM
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public List<CustomFieldEntry> CustomFields { get; set; } = new();
    }

    public class CustomFieldEntry
    {
        public int CustomFieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
