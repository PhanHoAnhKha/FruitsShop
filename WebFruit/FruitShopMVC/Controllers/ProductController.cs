using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using FruitShopMVC.DTOs;
using FruitShopMVC.Models;
using FruitShopMVC.ViewModels;

namespace FruitShopMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7163/api/");
        }

        private void SetAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SetAuthorizationHeader();
            List<Products> products = new List<Products>();
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetAllProducts/Get-All-Product");
            List<News> news = new List<News>();
            HttpResponseMessage response2 = await _httpClient.GetAsync($"New/GetAllNew");

            if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data = await response2.Content.ReadAsStringAsync();
                news = JsonConvert.DeserializeObject<List<News>>(data);
                string data2 = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(data2);
            }


            var viewModel = new HomeVM
            {
                Products = products ?? new List<Products>(),
                News = news ?? new List<News>()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Shop(string searchTerm)
        {
            List<Products> products = new List<Products>();
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetAllProducts/Get-All-Product");


            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                response = await _httpClient.GetAsync($"Product/GetAllProducts/Get-All-Product");
            }
            else
            {
                response = await _httpClient.GetAsync($"Product/SearchProductsByName?productName={searchTerm}");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(data);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Product/GetProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Products product = JsonConvert.DeserializeObject<Products>(data);
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Product/DeleteProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EmailSubscribe(string email)
        {
            try
            {
                var emailSubscription = new { Email = email };
                var content = new StringContent(JsonConvert.SerializeObject(emailSubscription), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Product/EmailSubscribe", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMessage = "Cảm ơn bạn đã đăng ký!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Không thể thêm đăng ký email.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý yêu cầu của bạn.");
            }
        }
    }
}
