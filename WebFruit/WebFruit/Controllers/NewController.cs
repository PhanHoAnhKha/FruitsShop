using Microsoft.AspNetCore.Mvc;
using WebFruit.Data;
using WebFruit.DTOs;
using WebFruit.Models;
using WebFruit.Interfaces;
namespace WebFruit.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NewController : ControllerBase
    {
        private readonly FruitDbContext _context;
        private readonly INewRepository _newRepository;
        public NewController(FruitDbContext context, INewRepository blogRepository)
        {
            _context = context;
            _newRepository = blogRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNew()
        {
            var blogs = await _newRepository.GetNewsAsync();
            if (blogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No news found matching the criteria.");
            }
            return StatusCode(StatusCodes.Status200OK, blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNew(int id)
        {
            var blog = await _newRepository.GetNewsByIdAsync(id);
            if (blog == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No new found for id: {id}");
            }
            return StatusCode(StatusCodes.Status200OK, blog);
        }

        [HttpPost]
        public async Task<ActionResult<New>> AddNew(NewDTO newDTO)
        {
            var result = await _newRepository.AddNewsAsync(newDTO);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add new.");
            }

            return StatusCode(StatusCodes.Status200OK, "New added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNew(int id, [FromBody] NewDTO newDTO)
        {
            var result = await _newRepository.UpdateNewsAsync(id, newDTO);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNew(int id)
        {
            var result = await _newRepository.DeleteNewsAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, "News deleted successfully");
        }
    }
}
