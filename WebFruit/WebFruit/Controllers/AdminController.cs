using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFruit.Models;

namespace WebFruit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("AllUsersAndRoles")]
        public async Task<IActionResult> AllUsersAndRoles()
        {
            var usersWithRoles = await _userManager.Users.ToListAsync();
            var result = new List<UserWithRolesVM>();

            foreach (var user in usersWithRoles)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserWithRolesVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(result);
        }

        [HttpGet("EditUserRoles/{userId}")]
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserWithRolesVM
            {
                UserId = userId,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles.ToList()
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles([FromBody] UserWithRolesVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (model.Roles == null || !model.Roles.Any())
            {
                model.Roles = new List<string> { "User" };
            }

            var rolesToRemove = userRoles.Except(model.Roles);
            var rolesToAdd = model.Roles.Except(userRoles);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                return BadRequest(removeResult.Errors);
            }

            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
            {
                return BadRequest(addResult.Errors);
            }

            return Ok(new { message = "User roles updated successfully" });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User deleted successfully" });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> UserDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userWithRoles = new UserWithRolesVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles
            };

            return Ok(userWithRoles);
        }
    }
}
