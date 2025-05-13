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
    }
}
