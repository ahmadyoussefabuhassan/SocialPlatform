using Microsoft.AspNetCore.Http.HttpResults;
using SocialPlatform.Application.Dtos.Comment;
using SocialPlatform.Application.Dtos.HelperException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Interface
{
    public interface ICommentSrevice
    {
        Task<int> GetCountCommentsByPostAsync(int postId);
        Task<Result<CommentsDto>> GetCommentById(int commentId);
        Task<int> GetCuontCommentsByUserAsync(int userId);
        Task<Result<CommentsDto>> AddCommentsAsync(CreateCommentsDto comment, int userId);
        Task DeleteCommentsAsync(int commentId);
        Task<Result<CommentsDto>> UpdateCommentsAsync(int commentId, UpdateCommentsDto dto);
        Task<Result<IEnumerable<CommentsDto>>> GetAllCommentsAsync();

    }
}
