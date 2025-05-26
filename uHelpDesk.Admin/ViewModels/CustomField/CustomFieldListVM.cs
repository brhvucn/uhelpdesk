namespace uHelpDesk.Admin.ViewModels.CustomField
{
    public class CustomFieldListVM
    {
        public string EntityName { get; set; }
        public List<uHelpDesk.Models.CustomField> Fields { get; set; } = new();
    }
}