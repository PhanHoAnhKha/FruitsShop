using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using FruitShopMVC.Models;
using FruitShopMVC.DTOs;

namespace FruitShopMVC.Controllers
{
    public class NewController : Controller
    {
        private readonly HttpClient _httpClient;
        public NewController(IHttpClientFactory httpClientFactory)
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
            List<News> News = new List<News>();
            HttpResponseMessage response = await _httpClient.GetAsync($"New/GetAllNew");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                News = JsonConvert.DeserializeObject<List<News>>(data);
            }

            return View(News);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            SetAuthorizationHeader();
            HttpResponseMessage response = await _httpClient.GetAsync($"New/GetNew/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                News book = JsonConvert.DeserializeObject<News>(data);
                return View(book);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(NewDTO blog)
        {
            SetAuthorizationHeader();

            var content = new StringContent(JsonConvert.SerializeObject(blog), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("New/AddNew", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "New");
            }
            else
            {
                return View(blog);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SetAuthorizationHeader();

            HttpResponseMessage response = await _httpClient.GetAsync($"New/GetNew/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                NewDTO book = JsonConvert.DeserializeObject<NewDTO>(data);
                return View(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewDTO news)
        {
            SetAuthorizationHeader();

            var content = new StringContent(JsonConvert.SerializeObject(news), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"Blog/UpdateNew/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "New");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể chỉnh sửa new. Vui lòng thử lại sau.");
                return View(news);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            SetAuthorizationHeader();

            HttpResponseMessage response = await _httpClient.DeleteAsync($"New/DeleteNew/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "New");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
