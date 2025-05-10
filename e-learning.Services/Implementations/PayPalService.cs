using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace e_learning.Services.Implementations
{

    public class PayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PayPalService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var clientId = _config["PayPal:ClientId"];
            var secret = _config["PayPal:Secret"];

            var byteArray = Encoding.UTF8.GetBytes($"{clientId}:{secret}");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var body = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

            var response = await _httpClient.PostAsync($"{_config["PayPal:BaseUrl"]}/v1/oauth2/token", body);
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("access_token").GetString()!;
        }

        public async Task<string> CreateOrderAsync(decimal amount, int studentId)
        {
            var token = await GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = amount.ToString("F2")
                    }
                }
            },
                application_context = new
                {
                    return_url = $"https://localhost:7180/api/Payment/paypal/success?studentId={studentId}",
                    cancel_url = "https://localhost:7180/api/Payment/paypal/cancel"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_config["PayPal:BaseUrl"]}/v2/checkout/orders", content);
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var approvalLink = doc.RootElement.GetProperty("links").EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href").GetString();

            return approvalLink!;
        }

        public async Task<bool> CaptureOrderAsync(string orderId)
        {
            var token = await GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync($"{_config["PayPal:BaseUrl"]}/v2/checkout/orders/{orderId}/capture", null);
            return response.IsSuccessStatusCode;
        }
    }

}