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

        // Index
        public async Task<IActionResult> Index()
        {
            var model = new ShowAllCustomersVM
            {
                Customers = await _customerFacade.GetAllCustomers()
            };
            return View(model);
        }

        // Show
        public async Task<IActionResult> Show(int id)
        {
            var customer = await _customerFacade.GetAllCustomers();
            var selectedCustomer = customer.FirstOrDefault(c => c.Id == id);

            if (selectedCustomer == null)
                return NotFound();

            return View(selectedCustomer);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = new Customer(model.Name, model.Email);
            await _customerFacade.CreateCustomer(customer);

            ShowSuccessMessage("Customer created successfully!");
            return RedirectToAction("Index");
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerFacade.GetAllCustomers();
            var selectedCustomer = customer.FirstOrDefault(c => c.Id == id);

            if (selectedCustomer == null)
                return NotFound();

            var model = new EditCustomerVM
            {
                Id = selectedCustomer.Id,
                Name = selectedCustomer.Name,
                Email = selectedCustomer.Email
            };

            return View(model);
        }

        // Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditCustomerVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = new Customer(model.Name, model.Email)
            {
                Id = model.Id
            };

            await _customerFacade.UpdateCustomer(customer);

            ShowSuccessMessage("Customer updated successfully!");
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerFacade.GetAllCustomers();
            var selectedCustomer = customer.FirstOrDefault(c => c.Id == id);

            if (selectedCustomer == null)
                return NotFound();

            return View(selectedCustomer);
        }

        // Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _customerFacade.DeleteCustomer(id);

            ShowSuccessMessage("Customer deleted successfully!");
            return RedirectToAction("Index");
        }
    }
}
