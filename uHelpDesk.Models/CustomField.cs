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
        //empty constructor for EF
        public CustomField() 
        { 
            //defaults
            this.FieldName = "New Field";
            this.FieldType = FieldTypes.Text;
            this.EntityType = EntityTypes.Customer;
        }
        public CustomField(string name, string entityType, string fieldType)
        {
            Ensure.That(name, nameof(name)).IsNotNullOrEmpty();
            Ensure.That(entityType, nameof(entityType)).IsNotNullOrEmpty();
            Ensure.That(fieldType, nameof(fieldType)).IsNotNullOrEmpty();
            this.FieldName = name;   
            this.FieldType = fieldType;
            this.EntityType = entityType;
        }        
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsActive { get; set; } = true;
        public string EntityType { get; set; }
    }

    public static class EntityTypes
    {
        public const string Customer = "Customer";
        public const string Ticket = "Ticket";
    }

    public static class FieldTypes
    {
        public const string Text = "Text";
        public const string Number = "Number";
    }
}
