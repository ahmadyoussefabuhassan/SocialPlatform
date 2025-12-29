using SocialPlatform.Application.Dtos.HelperException;
using SocialPlatform.Application.Dtos.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Interface
{
    public interface IPostService
    {
        Task<Result<PostsDto>> AddPostsAsync(CreatePostsDto dto , int userId);
        Task<Result<PostsDto>> UpdatePostsAsync(int postId, CreatePostsDto dto);
        Task DeletePostsAsync(int postId);
        Task<Result<PostsDto>> GetPostsAsync(int postId);
        Task<IEnumerable<Result<PostsDto>>> GetAllAsyne();
        Task<IEnumerable<Result<PostsDto>>> SearchPostsByContetAsyne(string Content);
    }
}
