using Newtonsoft.Json;

namespace FruitShopMVC.Models.Services
{
    public class RecommendationService
    {
        private readonly HttpClient _httpClient;

        public RecommendationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Products>> GetRecommendedProductsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"Product/Get-Recommended-Products/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Products>>(responseData);
            }
            return new List<Products>();
        }
    }
}
