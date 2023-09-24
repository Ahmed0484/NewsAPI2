using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models.DTOs;
using NewsAPI.Repositories;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepo _repo;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepo repo)
        {
            _userManager = userManager;
            _repo = repo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = userDto.Username,
                Email = userDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, userDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (userDto.Roles != null && userDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, userDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }
        // POST: /api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, userDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = _repo.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }

                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
