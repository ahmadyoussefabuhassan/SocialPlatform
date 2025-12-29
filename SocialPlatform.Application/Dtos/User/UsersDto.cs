using SocialPlatform.Application.Dtos.Comment;
using SocialPlatform.Application.Dtos.Post;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.User
{
    public class UsersDto 
    {
        public int Id { get; set; }
        public required string UserName { get; set; } 
        public required string Email { get; set; }
        public bool IsBanned { get; set; } 
        public required Roles Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<PostsDto>? Posts { get; set; } = new List<PostsDto>();
        public List<CommentsDto>? Comments { get; set; } = new List<CommentsDto>();
        public List<UserBansDto>? UserBans { get; set; } = new List<UserBansDto>();
    }
}
