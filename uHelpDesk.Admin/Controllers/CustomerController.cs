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
}