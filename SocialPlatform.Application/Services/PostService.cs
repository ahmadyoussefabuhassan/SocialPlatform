using AutoMapper;
using SocialPlatform.Application.Dtos.HelperException;
using SocialPlatform.Application.Dtos.Post;
using SocialPlatform.Application.Interface;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository,IMapper mapper)
            => (_postRepository,_mapper) = (postRepository,mapper);
        public async Task<Result<PostsDto>> AddPostsAsync(CreatePostsDto dto, int userId)
        {
            try
            {
                var post = _mapper.Map<Posts>(dto);
                post.Id = userId;
                post.CreatedAt = DateTime.Now;
                await _postRepository.AddAsync(post);
                return new Result<PostsDto>(true, _mapper.Map<PostsDto>(post));
            }
            catch (Exception ex)
            {
                return new Result<PostsDto>(false, null, ex.Message);
            }
        }

        public async Task DeletePostsAsync(int postId)
        {
            var post =  await _postRepository.GetByIdAsync(postId);
            if (post == null)
                throw new KeyNotFoundException("Post not found");
            await _postRepository.DeleteAsync(postId);
        }

        public async Task<IEnumerable<Result<PostsDto>>> GetAllAsyne()
        {
            try
            {
                var posts = await _postRepository.GetAllAsync();
                return posts.Select(post => new Result<PostsDto>(true, _mapper.Map<PostsDto>(post)));
            }
            catch (Exception ex)
            {

                return new List<Result<PostsDto>> { new Result<PostsDto>(false, null, ex.Message) };
            }
           
           
        }

        public async Task<Result<PostsDto>> GetPostsAsync(int postId)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(postId);
                if (post == null)
                    new Result<PostsDto>(false, null, "Post not Found");
                return new Result<PostsDto>(true, _mapper.Map<PostsDto>(post));
            }
            catch (Exception ex)
            {

                return new Result<PostsDto>(false, null, ex.Message);
            }
          
        }

        public async Task<IEnumerable<Result<PostsDto>>> SearchPostsByContetAsyne(string Content)
        {
            try
            {
                var posts = await _postRepository.GetPostsByContentAsync(Content);
                return posts.Select(post => new Result<PostsDto>(true, _mapper.Map<PostsDto>(post)));
            }
            catch (Exception ex)
            {
                return new List<Result<PostsDto>> { new Result<PostsDto>(false, null, ex.Message) };
            }
        }

        public async Task<Result<PostsDto>> UpdatePostsAsync(int postId, CreatePostsDto dto)
        {
            try
            {
               var post =  await _postRepository.GetByIdAsync(postId);
               if (post == null)
                   return new Result<PostsDto>(false, null, "Post not Found");

                _mapper.Map(dto, post);
                post.UpdatedAt = DateTime.Now;
                await _postRepository.UpdateAsync(post);
                return new Result<PostsDto>(true , _mapper.Map<PostsDto>(post));
            }
            catch (Exception ex)
            {

                return new Result<PostsDto>(false, null, ex.Message);
            }
        }

    }
}
