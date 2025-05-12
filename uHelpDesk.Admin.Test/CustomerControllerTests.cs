using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
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
        public void Dispose()
        {
            _customerFacadeMock = null;
            _controller = null;
            _tempData = null;
        }

    
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

    [Test]
    public async Task Index_ShouldReturnViewWithCustomers()
    {
        // Arrange
        var customers = new List<Customer> { new Customer("John Doe", "john@example.com") { Id = 1 } };
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

        // Act
        var result = await _controller.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        var model = result.Model as ShowAllCustomersVM;
        Assert.IsNotNull(model);
        Assert.AreEqual(1, model.Customers.Count());
    }

    [Test]
    public async Task Show_ShouldReturnView_WhenCustomerExists()
    {
        // Arrange
        var customers = new List<Customer> { new Customer("Alice", "alice@example.com") { Id = 5 } };
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

        // Act
        var result = await _controller.Show(5) as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Customer>(result.Model);
    }

    [Test]
    public async Task Show_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(new List<Customer>());

        var result = await _controller.Show(99);

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
        var customers = new List<Customer> { new Customer("Edit Me", "edit@example.com") { Id = 2 } };
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

        var result = await _controller.Edit(2) as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<EditCustomerVM>(result.Model);
    }

    [Test]
    public async Task Edit_Get_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(new List<Customer>());

        var result = await _controller.Edit(99);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_Post_ShouldRedirect_WhenModelIsValid()
    {
        var model = new EditCustomerVM { Id = 3, Name = "Edited", Email = "edited@example.com" };

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
        var customers = new List<Customer> { new Customer("Delete Me", "delete@me.com") { Id = 4 } };
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

        var result = await _controller.Delete(4) as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Customer>(result.Model);
    }

    [Test]
    public async Task Delete_Get_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        _customerFacadeMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(new List<Customer>());

        var result = await _controller.Delete(77);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task ConfirmDelete_ShouldRedirectToIndex()
    {
        var result = await _controller.ConfirmDelete(1);

        _customerFacadeMock.Verify(x => x.DeleteCustomer(1), Times.Once);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Customer deleted successfully!", _controller.TempData["success"]);
    }
    }
}
