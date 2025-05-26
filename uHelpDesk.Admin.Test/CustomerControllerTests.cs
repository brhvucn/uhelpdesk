using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uHelpDesk.Admin.Controllers;
using uHelpDesk.Admin.ViewModels.Customer;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.Admin.Test
{
    [TestFixture]
    public class CustomerControllerTests : IDisposable
    {
        private Mock<ICustomerFacade> _customerFacadeMock;
        private CustomerController _controller;
        private TempDataDictionary _tempData;

        [SetUp]
        public void Setup()
        {
            _customerFacadeMock = new Mock<ICustomerFacade>();

            var tempDataProvider = new Mock<ITempDataProvider>();
            var httpContext = new DefaultHttpContext();
            _tempData = new TempDataDictionary(httpContext, tempDataProvider.Object);

            _controller = new CustomerController(_customerFacadeMock.Object)
            {
                TempData = _tempData
            };
        }

        public void Dispose()
        {
            _customerFacadeMock = null;
            _controller = null;
            _tempData = null;
        }

        [Test]
        public async Task Index_ShouldReturnViewWithCustomers()
        {
            var customers = new List<Customer> { new Customer("John Doe", "john@example.com") { Id = 1 } };
            _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

            var result = await _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as ShowAllCustomersVM;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Customers.Count());
        }

        [Test]
        public async Task ShowCustomer_ShouldReturnView_WhenCustomerExists()
        {
            var customValues = new List<CustomFieldValue>();
            var customer = new Customer("Alice", "alice@example.com") { Id = 5, CustomValues = customValues };
            _customerFacadeMock.Setup(x => x.GetCustomerWithCustomFieldsByIdAsync(5)).ReturnsAsync(customer);
            _customerFacadeMock.Setup(x => x.GetAllCustomFieldsAsync()).ReturnsAsync(new List<CustomField>());

            var result = await _controller.ShowCustomer(5) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ShowCustomerVM>(result.Model);
        }

        [Test]
        public async Task ShowCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            _customerFacadeMock.Setup(x => x.GetCustomerWithCustomFieldsByIdAsync(99)).ReturnsAsync((Customer)null);

            var result = await _controller.ShowCustomer(99);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_Get_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Create_Post_ShouldRedirect_WhenModelIsValid()
        {
            var model = new CreateCustomerVM { Name = "New Customer", Email = "new@customer.com" };

            var result = await _controller.Create(model);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Customer created successfully!", _controller.TempData["success"]);
        }

        [Test]
        public async Task Create_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            var model = new CreateCustomerVM { Name = "", Email = "" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(model);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Edit_Get_ShouldReturnView_WhenCustomerExists()
        {
            var customer = new Customer("Edit Me", "edit@example.com") { Id = 2 };
            _customerFacadeMock.Setup(x => x.GetCustomerById(2)).ReturnsAsync(customer);

            var result = await _controller.Edit(2) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EditCustomerVM>(result.Model);
        }

        [Test]
        public async Task Edit_Get_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            _customerFacadeMock.Setup(x => x.GetCustomerById(99)).ReturnsAsync((Customer)null);

            var result = await _controller.Edit(99);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_Post_ShouldRedirect_WhenModelIsValid()
        {
            var model = new EditCustomerVM { Id = 3, Name = "Edited", Email = "edited@example.com" };
            _customerFacadeMock.Setup(x => x.UpdateCustomer(It.IsAny<Customer>())).ReturnsAsync(true);

            var result = await _controller.Edit(model);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Customer updated successfully!", _controller.TempData["success"]);
        }

        [Test]
        public async Task Edit_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            var model = new EditCustomerVM { Id = 3 };
            _controller.ModelState.AddModelError("Email", "Required");

            var result = await _controller.Edit(model);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Delete_Get_ShouldReturnView_WhenCustomerExists()
        {
            var customer = new Customer("Delete Me", "delete@me.com") { Id = 4 };
            _customerFacadeMock.Setup(x => x.GetCustomerById(4)).ReturnsAsync(customer);

            var result = await _controller.Delete(4) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Customer>(result.Model);
        }

        [Test]
        public async Task Delete_Get_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            _customerFacadeMock.Setup(x => x.GetCustomerById(77)).ReturnsAsync((Customer)null);

            var result = await _controller.Delete(77);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task ConfirmDelete_ShouldRedirectToIndex_WhenSuccessful()
        {
            _customerFacadeMock.Setup(x => x.DeleteCustomer(1)).ReturnsAsync(true);

            var result = await _controller.ConfirmDelete(1);

            _customerFacadeMock.Verify(x => x.DeleteCustomer(1), Times.Once);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Customer deleted successfully!", _controller.TempData["success"]);
        }
    }
}
