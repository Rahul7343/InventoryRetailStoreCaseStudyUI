using InventoryUi.Models;
using InventoryUi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryUi.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiService _productApi;
        private readonly SupplierApiService _supplierApi;

        public ProductController(ProductApiService productApi, SupplierApiService supplierApiService)
        {
            _productApi = productApi;
            _supplierApi = supplierApiService;
        }

        public async Task<IActionResult> GetProducts()
        {
            var products = await _productApi.GetAllProductsAsync();
            return PartialView("_ProductList", products);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productApi.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(long productId)
        {
            var product = await _productApi.GetProductAsync(productId);
            return View("Index", product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productApi.AddProductAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productApi.UpdateProductAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            var customer = await _productApi.DeleteProductAsync(productId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productApi.GetAllCategoriesAsync();
            return Json(categories);
        }
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierApi.GetAllSuppliersAsync();
            return Json(suppliers);
        }
    }

}
