using SocialPlatform.Application.Dtos.Comment;
using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.Post
{
    public class PostsDto : BaseEntity
    {
        public required string Title { get; set; } 
        public required string Content { get; set; } 
        public string ImageUrl { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<CommentsDto>? Comments { get; set; } = new List<CommentsDto>();
    }
}
