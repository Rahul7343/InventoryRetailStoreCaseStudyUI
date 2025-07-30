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
    public class CustomerApiService
    {
        private readonly HttpClient _httpClient;

        public CustomerApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Inventory");
        }

        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            var response = await _httpClient.GetAsync("api/Customer/GetCustomers");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CustomerModel>>(json);
            }

            return new List<CustomerModel>();
        }
        public async Task<CustomerModel> GetCustomerAsync(long customerId)
        {
            string url = $"api/Customer/GetCustomer/{customerId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CustomerModel>(json);
            }
            return new CustomerModel();
        }

        public async Task<bool> AddCustomerAsync(CustomerModel customerModel)
        {
            Customer customer = ToApiModel(customerModel);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(customer);
            var response = await _httpClient.PostAsync("api/Customer/AddCustomer", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCustomerAsync(CustomerModel customerModel)
        {
            Customer customer = ToApiModel(customerModel);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(customer);
            var response = await _httpClient.PutAsync("api/Customer/UpdateCustomer", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCustomerAsync(long customerId)
        {
            string url = $"api/Customer/DeleteCustomer/{customerId}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }
        private static Customer ToApiModel(CustomerModel customerModel)
        {
            Customer customer = new Customer();
            customer.Id = customerModel.Id;
            customer.CustomerName = customerModel.Name;
            customer.MobileNo = customerModel.MobileNo;
            customer.Address = customerModel.Address;
            customer.CreatedDate = customerModel.CreatedDate;
            customer.ModifiedDate = DateTime.Now;
            return customer;
        }
    }
}
