using InventoryUi.Models;
using InventoryUi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryUi.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierApiService _supplierApi;

        public SupplierController(SupplierApiService supplierApi)
        {
            _supplierApi = supplierApi;
        }

        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierApi.GetAllSuppliersAsync();
            return PartialView("_SupplierList", suppliers);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierApi.GetAllSuppliersAsync();
            return View(suppliers);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplier(long supplierId)
        {
            var supplier = await _supplierApi.GetSupplierAsync(supplierId);
            return View("Index", supplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApi.AddSupplierAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(SupplierModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApi.UpdateSupplierAsync(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSupplier(long supplierId)
        {
            var supplier = await _supplierApi.DeleteSupplierAsync(supplierId);
            return RedirectToAction("Index");
        }
    }

}
