using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.Comment
{
    public class CommentsDto : BaseEntity
    {
        public required string Text { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
