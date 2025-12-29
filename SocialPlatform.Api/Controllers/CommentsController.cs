using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialPlatform.Application.Dtos.Comment;
using SocialPlatform.Application.Interface;

namespace SocialPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentSrevice _commentService;
        public CommentsController(ICommentSrevice commentService)
            => _commentService = commentService;
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            var result = await _commentService.GetAllCommentsAsync();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("{commentId}")]
        [Authorize]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var result = await _commentService.GetCommentById(commentId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("count/post/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetCountCommentsByPostAsync(int postId)
        {
            try
            {
                var count = await _commentService.GetCountCommentsByPostAsync(postId);
                return Ok(new { PostId = postId, CommentCount = count });
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpGet("count/my-comments")]
        [Authorize]
        public async Task<IActionResult> GetCountCommentsByUserAsync()
        {
            var userId = GetCurrentUserId(); 
            if (userId == null) return Unauthorized();

            try
            {
                var count = await _commentService.GetCuontCommentsByUserAsync(userId.Value);
                return Ok(new { UserId = userId, CommentCount = count });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCommentsAsync([FromBody] CreateCommentsDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized("User not authenticated");
            var result = await _commentService.AddCommentsAsync(dto, userId.Value);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPut("{commentId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCommentsAsync(int commentId, [FromBody] UpdateCommentsDto dto)
        {
            var result = await _commentService.UpdateCommentsAsync(commentId, dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCommentsAsync(int commentId)
        {
            try
            {
                await _commentService.DeleteCommentsAsync(commentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
