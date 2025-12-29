using AutoMapper;
using SocialPlatform.Application.Dtos.Comment;
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
    public class CommentSrevice : ICommentSrevice
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public CommentSrevice(ICommentsRepository commentsRepository , IMapper mapper , IPostRepository postRepository)
            => (_commentsRepository ,_mapper ,_postRepository) =(commentsRepository, mapper,postRepository);
        public async Task<Result<CommentsDto>> AddCommentsAsync(CreateCommentsDto comment, int userId)
        {
            try
            {
                var comme = _mapper.Map<Comments>(comment);
                comme.UserId = userId;
                comme.CreatedAt = DateTime.Now;
                await _commentsRepository.AddAsync(comme);
                return new Result<CommentsDto>(true, _mapper.Map<CommentsDto>(comme));
            }
            catch (Exception ex)
            {
               return  new Result<CommentsDto>(false, null, ex.Message);

            }
        }

        public async Task DeleteCommentsAsync(int commentId)
        {
          var comment = await _commentsRepository.GetByIdAsync(commentId);
            if (comment == null)
                    throw new KeyNotFoundException("Comment not found");
            await _commentsRepository.DeleteAsync(commentId);

        }

        public async Task<Result<IEnumerable<CommentsDto>>> GetAllCommentsAsync()
        {
            try
            {
                var comments = await _commentsRepository.GetAllAsync();
                return new Result<IEnumerable<CommentsDto>>(true , _mapper.Map<IEnumerable<CommentsDto>>(comments));
            }
            catch (Exception)
            {

                return new Result<IEnumerable<CommentsDto>>(false, null, "An error occurred while retrieving comments");
            }
        }

        public async Task<Result<CommentsDto>> GetCommentById(int commentId)
        {
            try
            {
                var comment = await _commentsRepository.GetByIdAsync(commentId);
                if (comment == null)
                    return new Result<CommentsDto>(false, null, "Comment not found");
                return new Result<CommentsDto>(true, _mapper.Map<CommentsDto>(comment));
            }
            catch (Exception ex)
            {

                return new Result<CommentsDto>(false, null, ex.Message);
            }
            
        }

        public async Task<int> GetCountCommentsByPostAsync(int postId)
        {
            var count = await _postRepository.GetByIdAsync(postId);
            if (count == null)
                throw new KeyNotFoundException("Post not found");
            return await _commentsRepository.GetCountCommentsByPostAsync(postId);
        }

        public Task<int> GetCuontCommentsByUserAsync(int userId)
        {
            var count = _commentsRepository.GetCuontCommentsByUserAsync(userId);
            if (count == null)
                throw new KeyNotFoundException("User not found");
            return count;
        }

        public async Task<Result<CommentsDto>> UpdateCommentsAsync(int commentId, UpdateCommentsDto dto)
        {
            try
            {
                var comment = await _commentsRepository.GetByIdAsync(commentId);
                if (comment == null)
                    return new Result<CommentsDto>(false, null, "Comment not found");
                _mapper.Map(dto, comment);
                comment.UpdatedAt = DateTime.Now;
                await _commentsRepository.UpdateAsync(comment);
                return new Result<CommentsDto>(true, _mapper.Map<CommentsDto>(comment));
            }
            catch (Exception ex)
            {
                return new Result<CommentsDto>(false, null, ex.Message);
            }

        }
    }
}
