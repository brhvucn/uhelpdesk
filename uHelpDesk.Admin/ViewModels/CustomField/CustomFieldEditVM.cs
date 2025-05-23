using uHelpDesk.Models;

namespace uHelpDesk.Admin.ViewModels.CustomField
{
    public class CustomFieldEditVM
    {
        public int Id { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = FieldTypes.Text;
        public string EntityType { get; set; } = EntityTypes.Customer;
        public bool IsActive { get; set; } = true;
    }
}