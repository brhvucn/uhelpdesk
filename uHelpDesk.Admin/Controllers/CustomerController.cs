using Microsoft.AspNetCore.Mvc;
using uHelpDesk.Admin.ViewModels.Customer;
using uHelpDesk.BLL.Contracts;
using uHelpDesk.Models;
using uHelpDesk.Admin.ViewModels.Customer;

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
            var model = new ShowAllCustomersVM
            {
                Customers = await _customerFacade.GetAllCustomers()
            };
            return View(model);
        }

        public async Task<IActionResult> Show(int id)
        {
            var customer = await _customerFacade.GetCustomerById(id);
            if (customer == null)
            {
                ShowFailMessage("Customer not found.");
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                ShowFailMessage("Please correct the errors in the form.");
                return View(model);
            }

            var customer = new Customer(model.Name, model.Email);
            await _customerFacade.CreateCustomer(customer);

            ShowSuccessMessage("Customer created successfully!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerFacade.GetCustomerById(id);
            if (customer == null)
            {
                ShowFailMessage("Customer not found.");
                return NotFound();
            }

            var model = new EditCustomerVM
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                ShowFailMessage("Please correct the errors in the form.");
                return View(model);
            }

            var customer = new Customer(model.Name, model.Email)
            {
                Id = model.Id
            };

            var success = await _customerFacade.UpdateCustomer(customer);

            if (success)
            {
                ShowSuccessMessage("Customer updated successfully!");
                return RedirectToAction("Index");
            }

            ShowFailMessage("Failed to update customer.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerFacade.GetCustomerById(id);
            if (customer == null)
            {
                ShowFailMessage("Customer not found.");
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var success = await _customerFacade.DeleteCustomer(id);
            if (success)
            {
                ShowSuccessMessage("Customer deleted successfully!");
            }
            else
            {
                ShowFailMessage("Failed to delete customer.");
            }

            return RedirectToAction("Index");
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
}
