using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uHelpDesk.Models;
using uHelpDesk.Admin.ViewModels;
using uHelpDesk.Admin.ViewModels.CustomField;
using uHelpDesk.BLL.Contracts;

namespace uHelpDesk.Admin.Controllers
{
    [Authorize]
    public class CustomFieldController : BaseController
    {
        private readonly ICustomFieldFacade _facade;

        public CustomFieldController(ICustomFieldFacade facade)
        {
            _facade = facade;
        }

        public async Task<IActionResult> Index()
        {
            var fields = await _facade.GetCustomFieldsForEntityAsync(EntityTypes.Customer);
            return View(fields);
        }

        public IActionResult Create()
        {
            return View(new CustomFieldEditVM
            {
                EntityType = EntityTypes.Customer,
                FieldType = FieldTypes.Text,
                IsActive = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomFieldEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                ShowFailMessage("Please correct the errors before saving.");
                return View(vm);
            }

            var field = new CustomField(vm.FieldName, vm.EntityType, vm.FieldType)
            {
                IsActive = vm.IsActive
            };

            var success = await _facade.CreateCustomFieldAsync(field);
            if (!success)
            {
                ShowFailMessage("Failed to create custom field.");
                return View(vm);
            }

            ShowSuccessMessage("Custom field created successfully.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var field = await _facade.GetCustomFieldByIdAsync(id);
            if (field == null)
            {
                ShowFailMessage("Custom field not found.");
                return RedirectToAction(nameof(Index));
            }

            return View(new CustomFieldEditVM
            {
                Id = field.Id,
                FieldName = field.FieldName,
                FieldType = field.FieldType,
                EntityType = field.EntityType,
                IsActive = field.IsActive
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomFieldEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                ShowFailMessage("Please correct the errors before saving.");
                return View(vm);
            }

            var field = await _facade.GetCustomFieldByIdAsync(vm.Id);
            if (field == null)
            {
                ShowFailMessage("Custom field not found.");
                return RedirectToAction(nameof(Index));
            }

            field.FieldName = vm.FieldName;
            field.FieldType = vm.FieldType;
            field.IsActive = vm.IsActive;

            var success = await _facade.UpdateCustomFieldAsync(field);
            if (!success)
            {
                ShowFailMessage("Failed to update custom field.");
                return View(vm);
            }

            ShowSuccessMessage("Custom field updated successfully.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var field = await _facade.GetCustomFieldByIdAsync(id);
            if (field == null)
            {
                ShowFailMessage("Custom field not found.");
                return RedirectToAction(nameof(Index));
            }

            await _facade.DeleteCustomFieldAsync(id);
            ShowSuccessMessage("Custom field deleted successfully.");
            return RedirectToAction(nameof(Index));
        }
    }
}
