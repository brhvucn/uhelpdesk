using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models.Test.CustomFields
{
    [TestFixture]
    public class CustomFieldTests
    {
        [Test]
        [TestCase(null, "Customer", "Text")]
        [TestCase("Test", null, "Text")]
        [TestCase("Test", "Customer", null)]
        public void Constructor_WithNullName_ThrowsArgumentNullException(string? name, string? entityType, string? fieldType)
        {
            // Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomField(name, entityType, fieldType));            
        }

        [Test]
        [TestCase("", "Customer", "Text")]
        [TestCase("Test", "", "Text")]
        [TestCase("Test", "Customer", "")]
        public void Constructor_WithEmptyName_ThrowsArgumentException(string name, string entityType, string fieldType)
        {
            // Assert
            var ex = Assert.Throws<ArgumentException>(() => new CustomField(name, entityType, fieldType));            
        }

        [Test]
        public void Constructor_WithValidArguments_InitializesPropertiesCorrectly()
        {
            // Arrange
            var validName = "Customer";
            string validEntityType = "Customer";
            string validFieldType = "Text";

            // Act
            var field = new CustomField(validName, validEntityType, validFieldType);

            // Assert
            Assert.That(field.FieldName, Is.EqualTo(validName));
            Assert.That(field.EntityType, Is.EqualTo(validEntityType));
            Assert.That(field.FieldType, Is.EqualTo(validFieldType));
        }
    }
}
