﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebFruit.DTOs;
using WebFruit.Interfaces;

namespace WebFruit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AccountController(ITokenRepository tokenRepository, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            if (registerRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid registration request.");
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    var rolesResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (!rolesResult.Succeeded)
                    {
                        return BadRequest("Failed to add roles to the user.");
                    }
                }
                return Ok("Registration successful! Please login.");
            }

            var errors = identityResult.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid login request.");
            }

            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                    var response = new LoginResponseDTO
                    {
                        JwtToken = jwtToken
                    };

                    return Ok(response);
                }
            }

            return Unauthorized("Username or password incorrect.");
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO changePasswordRequestDTO)
        {
            if (changePasswordRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid change password request.");
            }

            var user = await _userManager.FindByNameAsync(changePasswordRequestDTO.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordRequestDTO.CurrentPassword, changePasswordRequestDTO.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                return Ok("Password changed successfully.");
            }

            var errors = changePasswordResult.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }
    }
}
