using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uHelpDesk.Admin.Controllers;
using uHelpDesk.Admin.ViewModels.CustomField;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.Tests.Admin.Controllers
{
    [TestFixture]
    public class CustomFieldControllerTests : IDisposable
    {
        private Mock<ICustomFieldFacade> _facadeMock;
        private CustomFieldController _controller;
        private DefaultHttpContext _httpContext;

        [SetUp]
        public void Setup()
        {
            _facadeMock = new Mock<ICustomFieldFacade>();
            _controller = new CustomFieldController(_facadeMock.Object);
            _httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
            _controller.TempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(
                _httpContext,
                Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
        }

        [Test]
        public async Task Index_ReturnsViewWithModel()
        {
            var fields = new List<CustomField>
            {
                new CustomField("Name1", EntityTypes.Customer, FieldTypes.Text),
                new CustomField("Name2", EntityTypes.Customer, FieldTypes.Text)
            };

            _facadeMock.Setup(f => f.GetCustomFieldsForEntityAsync(EntityTypes.Customer))
                       .ReturnsAsync(fields);

            var result = await _controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as CustomFieldListVM;
            Assert.IsNotNull(model);
            Assert.AreEqual("Customer", model.EntityName);
            Assert.AreEqual(2, model.Fields.Count);
        }

        [Test]
        public void Create_Get_ReturnsViewWithDefaultVM()
        {
            var result = _controller.Create() as ViewResult;
            Assert.IsNotNull(result);

            var vm = result.Model as CustomFieldEditVM;
            Assert.IsNotNull(vm);
            Assert.AreEqual(EntityTypes.Customer, vm.EntityType);
            Assert.AreEqual(FieldTypes.Text, vm.FieldType);
            Assert.IsTrue(vm.IsActive);
        }

        [Test]
        public async Task Create_Post_InvalidModel_ReturnsViewWithErrors()
        {
            _controller.ModelState.AddModelError("FieldName", "Required");

            var vm = new CustomFieldEditVM();

            var result = await _controller.Create(vm) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(vm, result.Model);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Create_Post_ValidModel_CreatesFieldAndRedirects()
        {
            var vm = new CustomFieldEditVM
            {
                FieldName = "NewField",
                EntityType = EntityTypes.Customer,
                FieldType = FieldTypes.Text,
                IsActive = true
            };

            _facadeMock.Setup(f => f.CreateCustomFieldAsync(It.IsAny<CustomField>()))
                       .ReturnsAsync(true);

            var result = await _controller.Create(vm) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("success"));
        }

        [Test]
        public async Task Create_Post_CreateFails_ReturnsViewWithError()
        {
            var vm = new CustomFieldEditVM
            {
                FieldName = "NewField",
                EntityType = EntityTypes.Customer,
                FieldType = FieldTypes.Text,
                IsActive = true
            };

            _facadeMock.Setup(f => f.CreateCustomFieldAsync(It.IsAny<CustomField>()))
                       .ReturnsAsync(false);

            var result = await _controller.Create(vm) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(vm, result.Model);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Edit_Get_FieldExists_ReturnsViewWithModel()
        {
            var field = new CustomField("Field1", EntityTypes.Customer, FieldTypes.Text)
            {
                Id = 1,
                IsActive = true
            };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync(field);

            var result = await _controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);

            var vm = result.Model as CustomFieldEditVM;
            Assert.IsNotNull(vm);
            Assert.AreEqual(1, vm.Id);
            Assert.AreEqual("Field1", vm.FieldName);
        }

        [Test]
        public async Task Edit_Get_FieldNotFound_RedirectsToIndexWithError()
        {
            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync((CustomField)null);

            var result = await _controller.Edit(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Edit_Post_InvalidModel_ReturnsViewWithError()
        {
            _controller.ModelState.AddModelError("FieldName", "Required");

            var vm = new CustomFieldEditVM();

            var result = await _controller.Edit(vm) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(vm, result.Model);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Edit_Post_FieldNotFound_RedirectsToIndexWithError()
        {
            var vm = new CustomFieldEditVM { Id = 1 };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(vm.Id))
                       .ReturnsAsync((CustomField)null);

            var result = await _controller.Edit(vm) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Edit_Post_ValidModel_UpdatesFieldAndRedirects()
        {
            var field = new CustomField("OldName", EntityTypes.Customer, FieldTypes.Text)
            {
                Id = 1,
                IsActive = true
            };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync(field);

            _facadeMock.Setup(f => f.UpdateCustomFieldAsync(field))
                       .ReturnsAsync(true);

            var vm = new CustomFieldEditVM
            {
                Id = 1,
                FieldName = "Updated",
                FieldType = FieldTypes.Text, 
                IsActive = true
            };

            var result = await _controller.Edit(vm) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("success"));
        }

        [Test]
        public async Task Edit_Post_UpdateFails_ReturnsViewWithError()
        {
            var field = new CustomField("OldName", EntityTypes.Customer, FieldTypes.Text)
            {
                Id = 1,
                IsActive = true
            };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync(field);

            _facadeMock.Setup(f => f.UpdateCustomFieldAsync(field))
                       .ReturnsAsync(false);

            var vm = new CustomFieldEditVM
            {
                Id = 1,
                FieldName = "Updated",
                FieldType = FieldTypes.Text,
                IsActive = true
            };

            var result = await _controller.Edit(vm) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(vm, result.Model);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Delete_Post_FieldNotFound_RedirectsToIndexWithError()
        {
            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync((CustomField)null);

            var result = await _controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Delete_Post_DeleteFails_ShowsError()
        {
            var field = new CustomField("Field1", EntityTypes.Customer, FieldTypes.Text)
            {
                Id = 1,
                IsActive = true
            };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync(field);

            _facadeMock.Setup(f => f.DeleteCustomFieldAsync(1))
                       .ReturnsAsync(false);

            var result = await _controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("error"));
        }

        [Test]
        public async Task Delete_Post_DeleteSucceeds_ShowsSuccess()
        {
            var field = new CustomField("Field1", EntityTypes.Customer, FieldTypes.Text)
            {
                Id = 1,
                IsActive = true
            };

            _facadeMock.Setup(f => f.GetCustomFieldByIdAsync(1))
                       .ReturnsAsync(field);

            _facadeMock.Setup(f => f.DeleteCustomFieldAsync(1))
                       .ReturnsAsync(true);

            var result = await _controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsTrue(_controller.TempData.ContainsKey("success"));
        }

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
