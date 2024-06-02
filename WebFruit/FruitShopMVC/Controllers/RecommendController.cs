using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FruitShopMVC.Models;
namespace FruitShopMVC.Controllers
{
    public class RecommendController : Controller
    {
        private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RecommendController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7163/api/");
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> Index()
        {
			var userId = _httpContextAccessor.HttpContext.Session.GetInt32("userId");
			if (!userId.HasValue)
			{
				return RedirectToAction("Login", "Account");
			}

			var response = await _httpClient.GetAsync($"Product/Get-Recommended-Products/{userId}");
			if (response.IsSuccessStatusCode)
			{
				var responseData = await response.Content.ReadAsStringAsync();
				var products = JsonConvert.DeserializeObject<List<Products>>(responseData);
				return View(products);
			}
			else
			{
				return View(new List<Products>());
			}
		}
    }
}
