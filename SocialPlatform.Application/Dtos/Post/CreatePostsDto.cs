using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.Post
{
    public class CreatePostsDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
