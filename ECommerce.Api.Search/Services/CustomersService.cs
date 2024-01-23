using System.Text.Json;
using ECommerce.Api.Search.Interface;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
