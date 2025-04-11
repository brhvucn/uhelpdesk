using Microsoft.Extensions.Logging;
using Moq;
using uHelpDesk.Admin.Controllers;
using uHelpDesk.Admin.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace uHelpDesk.Admin.Test
{
    public class AuthenticationTests : IDisposable
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<ILogger<AccountController>> _loggerMock;
        private AccountController _controller;
        
        public void Dispose()
        {
            _authServiceMock = null;
            _loggerMock = null;
            _controller = null;
        }

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AccountController>>();
            _controller = new AccountController(_authServiceMock.Object, _loggerMock.Object);
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        [Test]
        public void Login_Should_Return_View_With_Model()
        {
            var result = _controller.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<uHelpDesk.Admin.ViewModels.Account.LoginVM>(result.Model);
        }

        [Test]
        public void Login_WithError_Should_Set_ModelError()
        {
            var result = _controller.Login(true) as ViewResult;

            var model = result.Model as uHelpDesk.Admin.ViewModels.Account.LoginVM;

            Assert.AreEqual("Cannot log in with this combination of username and password", model.Error);
        }

        [Test]
        public async Task DoLogin_WithValidCredentials_ShouldRedirectToHome()
        {
            // Arrange
            var context = _controller.ControllerContext.HttpContext;
            context.Request.Form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "username", "test@test.com" },
                { "password", "password123" }
            });

            _authServiceMock
                .Setup(x => x.PasswordSignInAsync("test@test.com", "password123", true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.DoLogin();

            // Assert
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);
            Assert.AreEqual("Home", redirect.ControllerName);
        }

        [Test]
        public async Task DoLogin_WithInvalidCredentials_ShouldRedirectToLoginWithError()
        {
            // Arrange
            var context = _controller.ControllerContext.HttpContext;
            context.Request.Form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "username", "wrong@user.com" },
                { "password", "wrongpass" }
            });

            _authServiceMock
                .Setup(x => x.PasswordSignInAsync("wrong@user.com", "wrongpass", true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.DoLogin();

            // Assert
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Login", redirect.ActionName);
            Assert.IsTrue((bool)redirect.RouteValues["hasError"]);
        }

        [Test]
        public async Task Logout_ShouldRedirectToHome()
        {
            // Act
            var result = await _controller.Logout();

            // Assert
            var redirect = result as RedirectToActionResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.ActionName);
            Assert.AreEqual("Home", redirect.ControllerName);
        }
    }
}
