using Microsoft.AspNetCore.Mvc;
using uHelpDesk.Admin.ViewModels.Customer;
using uHelpDesk.BLL.Contracts;

namespace uHelpDesk.Admin.Controllers;

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

        var vm = new EditCustomerCustomFieldsVm
        {
            CustomerId = customer.Id,
            CustomerName = customer.Name,
            CustomFields = allFields.Select(field =>
            {
                var value = customer.CustomFieldValues?
                    .FirstOrDefault(v => v.CustomFieldId == field.Id)?.Value;

                return new CustomFieldEntry
                {
                    FieldId = field.Id,

                }

            }
        }
    }
}