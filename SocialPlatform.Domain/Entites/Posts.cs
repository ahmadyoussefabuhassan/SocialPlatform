using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Entites
{
    public class Posts : BaseEntity
    {
        [MaxLength(255)]
        public required string Title { get; set; }
        [MaxLength(255)]
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }
        public Users? Users { get; set; }
        public ICollection<Comments>? Comments { get; set; } = new List<Comments>();
    }
}
