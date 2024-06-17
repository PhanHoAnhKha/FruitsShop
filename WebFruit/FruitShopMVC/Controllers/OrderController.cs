using FruitShopMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FruitShopMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
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
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Orders order)
        {
            SetAuthorizationHeader();
            try
            {
                var jsonOrder = JsonConvert.SerializeObject(order);
                var content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Order/PlaceOrder", content);
                response.EnsureSuccessStatusCode();

                var clearCartResponse = await _httpClient.DeleteAsync("ShoppingCart/ClearCart");
                clearCartResponse.EnsureSuccessStatusCode();

                HttpContext.Session.SetInt32("CartCount", 0);

                return RedirectToAction("CheckoutComplete");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi khi đặt hàng: {ex.Message}";
                return View("Error");
            }
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }

        public async Task<IActionResult> AllOrder()
        {
            try
            {
                var response = await _httpClient.GetAsync("GetAllOrders");
                response.EnsureSuccessStatusCode();
                var allOrders = await response.Content.ReadAsAsync<IEnumerable<Orders>>();
                return View(allOrders);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error getting all orders: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> CompletedOrders()
        {
            try
            {
                SetAuthorizationHeader();
                string userId = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(userId))
                {
                    ViewBag.ErrorMessage = "Người dùng chưa đăng nhập.";
                    return View("Error");
                }
                var response = await _httpClient.GetAsync($"Order/GetCompletedOrders/{userId}");
                response.EnsureSuccessStatusCode();
                var completedOrders = await response.Content.ReadAsAsync<IEnumerable<Orders>>();
                return View(completedOrders);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error getting completed orders: {ex.Message}";
                return View("Error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Order/DeleteOrder/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error deleting order: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Order/DeleteOrder/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = $"Error deleting order: {response.ReasonPhrase}" });
            }
        }

    }
}
