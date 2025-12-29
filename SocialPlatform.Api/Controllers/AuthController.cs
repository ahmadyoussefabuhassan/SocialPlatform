using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialPlatform.Application.Dtos.User;
using SocialPlatform.Application.Interface;
using System.Security.Claims;

namespace SocialPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
            => _userService = userService;
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = GetCurrentUserId();
            var result = await _userService.GetUserProfileAsync(userId.Value);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsyne([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsyne(dto);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto updateDto)
        {
            var userId = GetCurrentUserId();
            var result = await _userService.UpdateUserProfileAsync(userId.Value, updateDto);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result);

        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LoguotAsync()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _userService.LoguotAsync(token);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpDelete("{userId}")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            var result = await _userService.DeleteUserProfileAsync(userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }

    }
}
