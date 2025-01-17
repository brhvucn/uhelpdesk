namespace uHelpDesk.Models.Test.Customer
{
    public class Tests
    {
        
        [TestCase("", "example@example.com")]        
        [TestCase("John Doe", "")]
        public void Constructor_WithInvalidParameters_ThrowsArgumentException(string? name, string? email)
        {
            var ex = Assert.Throws<ArgumentException>(() => new uHelpDesk.Models.Customer(name, email));
            Assert.That(ex.ParamName, Is.Not.Null);
        }

        [TestCase(null, "example@example.com")]        
        [TestCase("John Doe", null)]        
        public void Constructor_WithInvalidParameters_ThrowsArgumentNullException(string? name, string? email)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new uHelpDesk.Models.Customer(name, email));
            Assert.That(ex.ParamName, Is.Not.Null);
        }

        [TestCase("John Doe", "notanemail")]
        [TestCase("John Doe", "example.com")]
        public void Constructor_WithInvalidEmail_ThrowsArgumentException(string name, string email)
        {
            var ex = Assert.Throws<ArgumentException>(() => new uHelpDesk.Models.Customer(name, email));
            Assert.IsTrue(ex.Message.ToLower().Contains("email"));
        }

        [Test]
        public void Constructor_WithValidParameters_InitializesPropertiesCorrectly()
        {
            var name = "John Doe";
            var email = "john.doe@example.com";

            var customer = new uHelpDesk.Models.Customer(name, email);

            Assert.That(customer.Name, Is.EqualTo(name));
            Assert.That(customer.Email, Is.EqualTo(email));
        }

        // Add more tests if needed for other properties or methods
    }
}