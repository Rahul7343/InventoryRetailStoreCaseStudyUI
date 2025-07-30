using InventoryUi.ContractModel;
using InventoryUi.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InventoryUi.Services
{
    public class SupplierApiService
    {
        private readonly HttpClient _httpClient;

        public SupplierApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Inventory");
        }

        public async Task<List<SupplierModel>> GetAllSuppliersAsync()
        {
            var response = await _httpClient.GetAsync("api/Supplier/GetSuppliers");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SupplierModel>>(json);
            }

            return new List<SupplierModel>();
        }
        public async Task<SupplierModel> GetSupplierAsync(long supplierId)
        {
            string url = $"api/Supplier/GetSupplier/{supplierId}"; 

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SupplierModel>(json);
            }
            return new SupplierModel();
        }

        public async Task<bool> AddSupplierAsync(SupplierModel supplierModle)
        {
            Supplier supplier = ToApiModel(supplierModle);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(supplier);
            var response = await _httpClient.PostAsync("api/Supplier/AddSupplier", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateSupplierAsync(SupplierModel supplierModle)
        {
            Supplier supplier = ToApiModel(supplierModle);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(supplier);
            var response = await _httpClient.PutAsync("api/Supplier/UpdateSupplier", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteSupplierAsync(long supplierId)
        {
            string url = $"api/Supplier/DeleteSupplier/{supplierId}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }
        private static Supplier ToApiModel(SupplierModel supplierModle)
        {
            Supplier supplier = new Supplier();
            supplier.Id = supplierModle.Id;
            supplier.SupplierName = supplierModle.Name;
            supplier.MobileNo = supplierModle.MobileNo;
            supplier.CreatedDate = supplierModle.CreatedDate;
            supplier.ModifiedDate = DateTime.Now;
            return supplier;
        }
    }
}
