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
        public void Constructor_WithNullName_ThrowsArgumentNullException()
        {
            // Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomField(null));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Constructor_WithEmptyName_ThrowsArgumentException()
        {
            // Assert
            var ex = Assert.Throws<ArgumentException>(() => new CustomField(""));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Constructor_WithValidName_InitializesPropertiesCorrectly()
        {
            // Arrange
            var validName = "Status";

            // Act
            var field = new CustomField(validName);

            // Assert
            Assert.That(field.FieldName, Is.EqualTo(validName));
            Assert.IsNull(field.EntityType); // Asserting initial null value
        }

        [Test]
        public void Constructor_WithValidName_SetsFieldTypeToTextByDefault()
        {
            // Arrange
            var validName = "Status";

            // Act
            var field = new CustomField(validName);

            // Assert
            Assert.That(field.FieldType, Is.EqualTo("Text"));
        }

        [Test]
        public void Constructor_WithValidName_SetsIsActiveToTrueByDefault()
        {
            // Arrange
            var validName = "Status";

            // Act
            var field = new CustomField(validName);

            // Assert
            Assert.That(field.IsActive, Is.True);
        }

        [Test]
        public void SettingEntityType_AfterConstruction_SetsValueCorrectly()
        {
            // Arrange
            var field = new CustomField("Status");

            // Act
            field.EntityType = "Customer";

            // Assert
            Assert.That(field.EntityType, Is.EqualTo("Customer"));
        }
    }
}
