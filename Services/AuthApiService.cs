using InventoryUi.ContractModel;
using InventoryUi.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace InventoryUi.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Inventory");
        }

        public async Task<string> GetAuthTokenAsync()
        {
            LoginRequest login = new LoginRequest {Username ="admin", Password = "password" };
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var json = JsonConvert.SerializeObject(login);
            var response = await _httpClient.PostAsync("api/Auth/Login", jsonContent);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
