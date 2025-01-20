using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    public class CustomFieldValue : BaseModel
    {
        public CustomFieldValue()
        {
            //defaults
            this.Value = "New Value";
        }
        public CustomFieldValue(int customFieldId, string value, CustomField? field = null)
        {
            Ensure.That(customFieldId, nameof(customFieldId)).IsGt(0);
            Ensure.That(value, nameof(value)).IsNotNullOrEmpty();
            Ensure.That(field, nameof(field)).IsNotNull();
            this.CustomField = field;
            this.CustomFieldId = customFieldId;
            this.Value = value;
        }        
        public int CustomFieldId { get; set; }
        public CustomField? CustomField { get; set; }
        public string Value { get; set; }
        public int EntityId { get; set; }
        public int SortId { get; set; } = 0;
    }
}
