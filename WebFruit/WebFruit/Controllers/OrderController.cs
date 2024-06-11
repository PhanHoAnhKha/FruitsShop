using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFruit.Interfaces;
using WebFruit.Models;

namespace WebFruit.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            try
            {
                _orderRepository.PlaceOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderRepository.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetCompletedOrders(string userId)
        {
            try
            {
                var completedOrders = _orderRepository.GetCompletedOrders(userId);
                return Ok(completedOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                _orderRepository.DeleteOrder(orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllOrderDetail()
        {
            try
            {
                var orderdetail = _orderRepository.GetAllOrderDetail();
                return Ok(orderdetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
