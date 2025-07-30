using InventoryUi.Models;
using InventoryUi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryUi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerApiService _customerApi;

        public CustomerController(CustomerApiService customerApi)
        {
            _customerApi = customerApi;
        }

        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerApi.GetAllCustomersAsync();
            return PartialView("_CustomerList", customers);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerApi.GetAllCustomersAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(long customerId)
        {
            var customer = await _customerApi.GetCustomerAsync(customerId);
            return View("Index", customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerApi.AddCustomerAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerApi.UpdateCustomerAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(long customerId)
        {
            var customer = await _customerApi.DeleteCustomerAsync(customerId);
            return RedirectToAction("Index");
        }
    }

}
