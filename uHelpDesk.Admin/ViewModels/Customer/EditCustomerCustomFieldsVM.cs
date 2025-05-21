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
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
