using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    public class CustomField : BaseModel
    {
        public CustomField(string name)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrEmpty();            
            this.FieldName = name;            
        }        
        public string FieldName { get; set; }
        public string FieldType { get; set; } = "Text";
        public bool IsActive { get; set; } = true;
        public string EntityType { get; set; } // "Customer" or "Ticket"
    }
}
