using Microsoft.AspNetCore.Mvc;
using WebFruit.DTOs;
using WebFruit.Interfaces;
using WebFruit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebFruit.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        #region Category Endpoints
        [HttpGet("Get-All-Category")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _productRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _productRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            var result = await _productRepository.AddCategoryAsync(categoryDTO);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add category.");
            }

            return StatusCode(StatusCodes.Status200OK, "Category added successfully");
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            var result = await _productRepository.UpdateCategoryAsync(id, categoryDTO);
            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _productRepository.DeleteCategoryAsync(id);
            if (!result)
                return BadRequest();

            return NoContent();
        }
        #endregion Category Endpoints

        #region Product Endpoints
        [HttpGet("Get-All-Product")]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("GetAll Product Action method was invoked");
            var products = await _productRepository.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No products found matching the criteria.");
            }

            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No product found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO)
        {
            var result = await _productRepository.AddProductAsync(productDTO);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product.");
            }

            return StatusCode(StatusCodes.Status200OK, "Products added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody] ProductDTO productDTO)
        {
            var result = await _productRepository.UpdateProductAsync(id, productDTO);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, "Product deleted successfully");
        }
        [HttpPost]
        public async Task<IActionResult> EmailSubscribe(EmailSubscribeDTO emailDto)
        {
            var result = await _productRepository.AddEmailSubscription(emailDto);

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Email subscription added successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add email subscription.");
            }
        }
        #endregion Product Endpoints
    }
}
