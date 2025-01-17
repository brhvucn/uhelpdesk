using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models.Test.TicketStatus
{
    [TestFixture]
    public class TicketStatusTest
    {
        [Test]
        public void Constructor_WithNullName_ThrowsArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new uHelpDesk.Models.TicketStatus(null));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Constructor_WithEmptyName_ThrowsArgumentException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new uHelpDesk.Models.TicketStatus(""));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Constructor_WithValidName_SetsNameProperty()
        {
            // Arrange
            var validName = "Active";

            // Act
            var ticketStatus = new uHelpDesk.Models.TicketStatus(validName);

            // Assert
            Assert.That(ticketStatus.Name, Is.EqualTo(validName));
        }

        [Test]
        public void Constructor_WithValidName_InitializesBaseModelProperties()
        {
            // Arrange
            var validName = "Active";

            // Act
            var ticketStatus = new uHelpDesk.Models.TicketStatus(validName);
            var creationTime = DateTime.UtcNow;

            // Assert
            Assert.That(ticketStatus.CreatedAt, Is.LessThanOrEqualTo(creationTime));
            Assert.That(ticketStatus.UpdatedAt, Is.LessThanOrEqualTo(creationTime));
        }

        [Test]
        public void IsClosedStatus_DefaultsToFalse()
        {
            // Arrange
            var ticketStatus = new uHelpDesk.Models.TicketStatus("Active");

            // Assert
            Assert.That(ticketStatus.IsClosedStatus, Is.False);
        }

        [Test]
        public void SortOrder_DefaultsToZero()
        {
            // Arrange
            var ticketStatus = new uHelpDesk.Models.TicketStatus("Active");

            // Assert
            Assert.That(ticketStatus.SortOrder, Is.EqualTo(0));
        }

        [Test]
        public void Description_DefaultsToNull()
        {
            // Arrange
            var ticketStatus = new uHelpDesk.Models.TicketStatus("Active");

            // Assert
            Assert.That(ticketStatus.Description, Is.Null);
        }

        // Additional tests can be added to simulate changes to the properties and ensure they behave as expected
    }
}
