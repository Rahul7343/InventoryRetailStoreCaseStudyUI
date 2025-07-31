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
    public class SalesApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthApiService _authToken;
        public SalesApiService(IHttpClientFactory httpClientFactory, AuthApiService authToken)
        {
            _httpClient = httpClientFactory.CreateClient("Inventory");
            _authToken = authToken;
        }

        public async Task<List<SalesModel>> GetAllSalesAsync()
        {
            await GetAuthToken();
            var response = await _httpClient.GetAsync("api/Sales/GetSales");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SalesModel>>(json);
            }

            return new List<SalesModel>();
        }
        public async Task<SalesModel> GetSaleAsync(long saleId)
        {
            await GetAuthToken();
            string url = $"api/Sales/GetSale/{saleId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SalesModel>(json);
            }
            return new SalesModel();
        }

        public async Task<bool> AddSaleAsync(SalesModel saleModel)
        {
            Sales sale = ToApiModel(saleModel);
            await GetAuthToken();
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(sale), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(sale);
            var response = await _httpClient.PostAsync("api/Sales/AddSale", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateSaleAsync(SalesModel saleModel)
        {
            Sales sale = ToApiModel(saleModel);
            await GetAuthToken();
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(sale), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(sale);
            var response = await _httpClient.PutAsync("api/Sales/UpdateSale", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteSaleAsync(long saleId)
        {
            await GetAuthToken();
            string url = $"api/Sales/DeleteSale/{saleId}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }
        private static Sales ToApiModel(SalesModel saleModel)
        {
            Sales sale = new Sales();
            sale.Id = saleModel.Id;
            sale.Quantity = saleModel.Quantity;
            sale.CustomerId = saleModel.CustomerId;
            sale.Price = saleModel.Price;
            sale.PurchaseDate = saleModel.PurchaseDate;
            sale.ProductId = saleModel.ProductId;
            sale.CreatedDate = saleModel.CreatedDate;
            sale.ModifiedDate = DateTime.Now;
            return sale;
        }
        private async Task GetAuthToken()
        {
            var token = await _authToken.GetAuthTokenAsync();
            AuthorizationTokenResponse oAuthorizationTokenResponse = JsonConvert.DeserializeObject<AuthorizationTokenResponse>(token);
            if (!string.IsNullOrEmpty(oAuthorizationTokenResponse.token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oAuthorizationTokenResponse.token);
            }
        }
    }
}
