using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialPlatform.Application.Dtos.Post;
using SocialPlatform.Application.Interface;
using System.Security.Claims;

namespace SocialPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseApiController
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
            => _postService = postService;
        [HttpGet("{postId}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postService.GetPostsAsync(postId);
            if (post == null)
                return NotFound("Post not found");
            return Ok(post);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPostsAsyne()
        {
            var posts = await _postService.GetAllAsyne();
            return Ok(posts);
        }
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchPosts([FromQuery] string content)
        {
            var posts = await _postService.SearchPostsByContetAsyne(content);
            if(posts == null || !posts.Any())
                return NotFound("No posts found matching the search criteria");
            return Ok(posts);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostsDto dto)
        {
            var userId = GetCurrentUserId();
            var createdPost = await _postService.AddPostsAsync(dto,(int) userId);
            return CreatedAtAction(nameof(GetPostById), new { postId = createdPost.Data.Id }, createdPost);
        }
        [HttpPut("{postId}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] CreatePostsDto dto)
        {
            var updatedPost = await _postService.UpdatePostsAsync(postId, dto);
            if (updatedPost == null)
                return NotFound("Post not found");
            return Ok(updatedPost);
        }
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await _postService.GetPostsAsync(postId);
            if (post == null)
                return NotFound("Post not found");
            await _postService.DeletePostsAsync(postId);
            return NoContent();
        }
    }
}
