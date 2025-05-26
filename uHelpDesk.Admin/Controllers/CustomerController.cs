using Microsoft.AspNetCore.Mvc;
using uHelpDesk.Admin.ViewModels.Customer;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerFacade _customerFacade;

    public CustomerController(ICustomerFacade customerFacade)
    {
        this._customerFacade = customerFacade;
    }

    public async Task<IActionResult> Index()
    {
        ShowAllCustomersVM model = new ShowAllCustomersVM();
        model.Customers = await this._customerFacade.GetAllCustomers();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditCustomFields(int id)
    {
        var customer = await _customerFacade.GetCustomerWithCustomFieldsByIdAsync(id);
        if (customer == null)
            return NotFound();

        var allFields = await _customerFacade.GetAllCustomFieldsAsync();
        var existingValues = customer.CustomValues ?? new List<CustomFieldValue>();

        var vm = new EditCustomerCustomFieldsVM
        {
            CustomerId = customer.Id,
            CustomFields = allFields.Select(field =>
            {
                var value = existingValues.FirstOrDefault(v => v.CustomFieldId == field.Id);
                return new CustomFieldEntry
                {
                    CustomFieldId = field.Id,
                    FieldName = field.FieldName,
                    FieldType = field.FieldType,
                    Value = value?.Value ?? string.Empty
                };
            }).ToList()
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> ShowCustomer(int id)
    {
        var customer = await _customerFacade.GetCustomerWithCustomFieldsByIdAsync(id);
        if (customer == null)
            return NotFound();

        var allFields = await _customerFacade.GetAllCustomFieldsAsync();

        var customField = (customer.CustomValues ?? new List<CustomFieldValue>())
            .Select(val => new
            {
                FieldName = val.CustomField?.FieldName ?? "Unknown",
                Value = val.Value
            })
            .ToList();

        var vm = new ShowCustomerVM
        {
            Customer = customer,
            CustomFields = customField.ToDictionary(x => x.FieldName, x => x.Value),
            AvailableFields = allFields
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AssignCustomField(int id, ShowCustomerVM vm)
    {
        var values = new List<CustomFieldValue>
        {
            new CustomFieldValue
            {
                CustomFieldId = vm.SelectedCustomFieldId,
                Value = vm.CustomFieldValue,
                EntityId = id
            }
        };

        await _customerFacade.SaveCustomFieldValuesAsync(id, values);

        return RedirectToAction("ShowCustomer", new { id = id });
    }

    [HttpPost]
    public async Task<IActionResult> EditCustomFields(EditCustomerCustomFieldsVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var values = vm.CustomFields.Select(f => new CustomFieldValue
        {
            CustomFieldId = f.CustomFieldId,
            Value = f.Value,
            EntityId = vm.CustomerId
        }).ToList();

        await _customerFacade.SaveCustomFieldValuesAsync(vm.CustomerId, values);

        return RedirectToAction("Index");
    }
}
