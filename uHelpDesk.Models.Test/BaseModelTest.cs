using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models.Test
{
    [TestFixture]
    public class BaseModelTest
    {
        [Test]
        public void Constructor_WhenCalled_SetsCreatedAtToCurrentUtcTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var model = new BaseModel();

            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.That(model.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(model.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        }

        [Test]
        public void Constructor_WhenCalled_SetsUpdatedAtToCurrentUtcTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var model = new BaseModel();

            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.That(model.UpdatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(model.UpdatedAt, Is.LessThanOrEqualTo(afterCreation));
        }

        [Test]
        public void Constructor_WhenCalled_SetsCreatedAtEqualToUpdatedAt()
        {
            // Act
            var model = new BaseModel();

            // Assert
            Assert.That(model.UpdatedAt, Is.EqualTo(model.CreatedAt));
        }
    }
}
