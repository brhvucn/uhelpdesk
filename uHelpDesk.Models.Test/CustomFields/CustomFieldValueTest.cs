using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models.Test.CustomFields
{
    [TestFixture]
    public class CustomFieldValueTest
    {
        [Test]
        public void Constructor_WithInvalidCustomFieldId_ThrowsArgumentException()
        {
            // Arrange
            var invalidCustomFieldId = 0;
            var validValue = "Example Value";
            var validField = new CustomField("Valid Name");

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CustomFieldValue(invalidCustomFieldId, validValue, validField));
            Assert.That(ex.ParamName, Is.EqualTo("customFieldId"));
        }

        [Test]
        public void Constructor_WithNullValue_ThrowsArgumentNullException()
        {
            // Arrange
            var validCustomFieldId = 1;
            var nullValue = (string)null;
            var validField = new CustomField("Valid Name");

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomFieldValue(validCustomFieldId, nullValue, validField));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }

        [Test]
        public void Constructor_WithEmptyValue_ThrowsArgumentException()
        {
            // Arrange
            var validCustomFieldId = 1;
            var emptyValue = "";
            var validField = new CustomField("Valid Name");

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CustomFieldValue(validCustomFieldId, emptyValue, validField));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }

        [Test]
        public void Constructor_WithNullCustomField_ThrowsArgumentNullException()
        {
            // Arrange
            var validCustomFieldId = 1;
            var validValue = "Example Value";

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new CustomFieldValue(validCustomFieldId, validValue, null));
            Assert.That(ex.ParamName, Is.EqualTo("field"));
        }

        [Test]
        public void Constructor_WithValidParameters_InitializesPropertiesCorrectly()
        {
            // Arrange
            var validCustomFieldId = 1;
            var validValue = "Example Value";
            var validField = new CustomField("Valid Name");

            // Act
            var customFieldValue = new CustomFieldValue(validCustomFieldId, validValue, validField);

            // Assert
            Assert.That(customFieldValue.CustomFieldId, Is.EqualTo(validCustomFieldId));
            Assert.That(customFieldValue.Value, Is.EqualTo(validValue));
            Assert.That(customFieldValue.CustomField, Is.EqualTo(validField));
            Assert.That(customFieldValue.SortId, Is.EqualTo(0)); // Default value check
        }
    }
}
