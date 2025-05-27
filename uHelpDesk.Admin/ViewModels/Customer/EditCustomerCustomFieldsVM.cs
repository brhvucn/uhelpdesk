using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace uHelpDesk.Admin.ViewModels.Customer
{
    public class EditCustomerCustomFieldsVM : IValidatableObject
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public List<CustomFieldEntry> CustomFields { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var field in CustomFields)
            {
                if (!IsValidValue(field.FieldType, field.Value))
                {
                    yield return new ValidationResult(
                        $"Value for '{field.FieldName}' is invalid for type {field.FieldType}.",
                        new[] { nameof(CustomFields) });
                }
            }
        }

        private bool IsValidValue(string fieldType, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true; // allow empty values, adjust if needed

            switch (fieldType.ToLower())
            {
                case "number":
                    return decimal.TryParse(value, out _);
                case "date":
                    return DateTime.TryParse(value, out _);
                case "text":
                    return true;
                default:
                    return true;
            }
        }
    }

    public class CustomFieldEntry
    {
        public int CustomFieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}