using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.Comment
{
    public class CreateCommentsDto
    {
        public required string Text { get; set; }
        public int PostId { get; set; }
    }
}
