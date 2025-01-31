using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.Models;

namespace uHelpDesk.Models.Test.TicketAnswer
{
    [TestFixture]
    public class TicketAnswerTest
    {
        [Test]
        public void Constructor_ValidParameters_ShouldSetProperties()
        {
            // Arrange
            string description = "Printer does not work";
            string createdBy = "user123";

            // Act
            Models.TicketAnswer ticketAnswer = new Models.TicketAnswer(description, createdBy);

            // Assert
            Assert.AreEqual(description, ticketAnswer.Description);
            Assert.AreEqual(createdBy, ticketAnswer.CreatedBy);
            Assert.IsTrue(ticketAnswer.IsInternal);
            Assert.That(ticketAnswer.Created, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));
        }
        
        [TestCase("")]
        public void Constructor_InvalidDescription_ShouldThrowArgumentException(string? description)
        {
            // Arrange
            string createdBy = "user123";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Models.TicketAnswer(description, createdBy));
            Assert.That(ex.ParamName, Is.EqualTo("description"));
        }

        [TestCase(null)]        
        public void Constructor_InvalidDescription_ShouldThrowArgumentNullException(string? description)
        {
            // Arrange
            string createdBy = "user123";

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new Models.TicketAnswer(description, createdBy));
            Assert.That(ex.ParamName, Is.EqualTo("description"));
        }

        [TestCase("")]
        public void Constructor_InvalidCreatedBy_ShouldThrowArgumentException(string? createdBy)
        {
            // Arrange
            string description = "Printer does not work";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Models.TicketAnswer(description, createdBy));
            Assert.That(ex.ParamName, Is.EqualTo("createdBy"));
        }

        [TestCase(null)]        
        public void Constructor_InvalidCreatedBy_ShouldThrowArgumentNullException(string? createdBy)
        {
            // Arrange
            string description = "Printer does not work";

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new Models.TicketAnswer(description, createdBy));
            Assert.That(ex.ParamName, Is.EqualTo("createdBy"));
        }

        [Test]
        public void Constructor_IsInternalFalse_ShouldSetIsInternalToFalse()
        {
            // Arrange
            string description = "Printer does not work";
            string createdBy = "user123";
            bool isInternal = false;

            // Act
            Models.TicketAnswer ticketAnswer = new Models.TicketAnswer(description, createdBy, isInternal);

            // Assert
            Assert.IsFalse(ticketAnswer.IsInternal);
        }
    }
}
