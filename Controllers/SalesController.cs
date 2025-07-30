using InventoryUi.Models;
using InventoryUi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryUi.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesApiService _salesApi;
        private readonly ProductApiService _productApi;
        private readonly CustomerApiService _customerApi;


        public SalesController(SalesApiService salesApi, ProductApiService productApi, CustomerApiService customerApi)
        {
            _salesApi = salesApi;
            _productApi = productApi;
            _customerApi = customerApi;
        }

        public async Task<IActionResult> GetSales()
        {
            var sales = await _salesApi.GetAllSalesAsync();
            return PartialView("_SalesList", sales);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sales = await _salesApi.GetAllSalesAsync();
            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> GetSale(long saleId)
        {
            var sale = await _salesApi.GetSaleAsync(saleId);
            return View("Index", sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SalesModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _salesApi.AddSaleAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSale(SalesModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _salesApi.UpdateSaleAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSale(long saleId)
        {
            var sale = await _salesApi.DeleteSaleAsync(saleId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productApi.GetAllProductsAsync();
            return Json(products);
        }
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerApi.GetAllCustomersAsync();
            return Json(customers);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(long productId)
        {
            var product = await _productApi.GetProductAsync(productId);
            return Json(product);
        }
    }

}
