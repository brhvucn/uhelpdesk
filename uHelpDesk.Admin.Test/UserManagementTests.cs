using Moq;
using NUnit.Framework;
using uHelpDesk.Admin.Controllers;
using uHelpDesk.Admin.Services.Contracts;
using uHelpDesk.Admin.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using uHelpDesk.Admin.Services;

namespace uHelpDesk.Admin.Test
{
    [TestFixture]
    public class UserManagementTests : IDisposable
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<ILogger<AccountController>> _loggerMock;
        private AccountController _controller;
        private ITempDataDictionary _tempData;
        

        public void Dispose()
        {
            // Clean up resources
            _authServiceMock = null;
            _loggerMock = null;
            _controller = null;
            _tempData = null;
        }

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AccountController>>();

            // Set up TempData with a real implementation
            _tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            _controller = new AccountController(_authServiceMock.Object, _loggerMock.Object)
            {
                TempData = _tempData
            };
        }

        [Test]
        public async Task RegisterUser_ShouldRedirectToAccount_WhenUserIsCreated()
        {
            // Arrange
            var model = new RegisterUserVM { Email = "testuser@example.com", Password = "Password123", Role = "Administrator" };
            var result = IdentityResult.Success;
            _authServiceMock.Setup(x => x.RegisterUserAsync(model.Email, model.Password, model.Role)).ReturnsAsync(new CreateResult { Result = result });

            // Act
            var actionResult = await _controller.RegisterUser(model);

            // Assert
            var redirectResult = actionResult as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Assert that ShowSuccessMessage was called
            Assert.AreEqual("User successfully created.", _tempData["success"]);
        }

        [Test]
        public async Task RegisterUser_ShouldReturnView_WhenUserCreationFails()
        {
            // Arrange
            var model = new RegisterUserVM { Email = "testuser@example.com", Password = "Password123", Role = "Administrator" };
            var result = IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            _authServiceMock.Setup(x => x.RegisterUserAsync(model.Email, model.Password, model.Role)).ReturnsAsync(new CreateResult { Result = result });

            // Act
            var actionResult = await _controller.RegisterUser(model);

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(model, viewResult.Model);
            Assert.IsTrue(viewResult.ViewData.ModelState.Values.Any(v => v.Errors.Any()));

            // Assert that ShowFailMessage was called
            Assert.AreEqual("User creation failed.", _tempData["error"]);
        }

        [Test]
        public async Task EditUser_ShouldReturnView_WhenUserIsFound()
        {
            // Arrange
            var userId = "userId";
            var user = new IdentityUser { Id = userId, Email = "testuser@example.com" };
            _authServiceMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(user);
            _authServiceMock.Setup(x => x.GetRolesAsync(userId)).ReturnsAsync(new List<string> { "Administrator" });

            var model = new UserVM { Id = user.Id, Email = user.Email, Role = "Administrator" };

            // Act
            var result = await _controller.Edit(userId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var modelResult = viewResult.Model as UserVM;
            Assert.AreEqual(model.Id, modelResult.Id);
            Assert.AreEqual(model.Email, modelResult.Email);
            Assert.AreEqual(model.Role, modelResult.Role);
        }

        [Test]
        public async Task EditUser_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = "userId";
            _authServiceMock.Setup(x => x.GetUserById(userId)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Edit(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task EditUser_ShouldRedirectToAccount_WhenUserIsUpdated()
        {
            // Arrange
            var model = new UserVM { Id = "userId", Email = "updateduser@example.com", Role = "Administrator" };
            var user = new IdentityUser { Id = model.Id, Email = "olduser@example.com" };
            _authServiceMock.Setup(x => x.GetUserById(model.Id)).ReturnsAsync(user);
            _authServiceMock.Setup(x => x.GetRolesAsync(model.Id)).ReturnsAsync(new List<string> { "User" });
            _authServiceMock.Setup(x => x.UpdateUser(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Edit(model);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);

            // Assert that ShowSuccessMessage was called
            Assert.AreEqual("User updated successfully.", _tempData["success"]);
        }
        

        [Test]
        public async Task DeleteUser_ShouldRedirectToAccount_WhenUserIsDeleted()
        {
            // Arrange
            var userId = "userId";
            _authServiceMock.Setup(x => x.DeleteUserAsync(userId)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);

            // Assert that ShowSuccessMessage was called
            Assert.AreEqual("User deleted successfully.", _tempData["success"]);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnFailMessage_WhenDeletionFails()
        {
            // Arrange
            var userId = "userId";
            _authServiceMock.Setup(x => x.DeleteUserAsync(userId)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to delete user" }));

            // Act
            var result = await _controller.Delete(userId);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Account", redirectResult.ControllerName);

            // Assert that ShowFailMessage was called
            Assert.AreEqual("Failed to delete user.", _tempData["error"]);
        }
    }
}
 