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
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthApiService _authToken;
        public ProductApiService(IHttpClientFactory httpClientFactory, AuthApiService authToken)
        {
            _httpClient = httpClientFactory.CreateClient("Inventory");
            _authToken = authToken;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            await GetAuthToken();
            var response = await _httpClient.GetAsync("api/Product/GetProducts");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductModel>>(json);
            }

            return new List<ProductModel>();
        }
        public async Task<ProductModel> GetProductAsync(long productId)
        {
            await GetAuthToken();
            string url = $"api/Product/GetProduct/{productId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductModel>(json);
            }
            return new ProductModel();
        }

        public async Task<bool> AddProductAsync(ProductModel productModel)
        {
            await GetAuthToken();
            Product product = ToApiModel(productModel);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(product);
            var response = await _httpClient.PostAsync("api/Product/AddProduct", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProductAsync(ProductModel productModel)
        {
            Product product = ToApiModel(productModel);
            await GetAuthToken();
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(product);
            var response = await _httpClient.PutAsync("api/Product/UpdateProduct", jsonContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteProductAsync(long productId)
        {
            await GetAuthToken();
            string url = $"api/Product/DeleteProduct/{productId}";

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }
        private static Product ToApiModel(ProductModel productModel)
        {
            Product product = new Product();
            product.Id = productModel.Id;
            product.ProductName = productModel.ProductName;
            product.Price = productModel.ProductPrice;
            product.SupplierId = productModel.SupplierId;
            product.CategoryId = productModel.CategoryId;
            product.StockQuantity = productModel.StockQuantity;
            product.CreatedDate = productModel.CreatedDate;
            product.ModifiedDate = DateTime.Now;
            return product;
        }
        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            await GetAuthToken();
            var response = await _httpClient.GetAsync("api/Category/GetCategories");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CategoryModel>>(json);
            }

            return new List<CategoryModel>();
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
