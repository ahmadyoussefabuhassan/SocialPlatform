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
    public class Comments : BaseEntity
    {
        [MaxLength(500)]
        public required string Text { get; set; }
        public int UserId { get; set; }
        public Users? Users { get; set; }
        public int PostId { get; set; }
        public Posts? Posts { get; set; }
    }
}
