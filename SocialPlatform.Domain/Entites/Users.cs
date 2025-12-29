using Microsoft.AspNetCore.Identity;
using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Entites
{
    public class Users : IdentityUser<int>
    {
        public bool IsBanned { get; set; }
        public required Roles Role { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Posts>? Posts { get; set; } = new List<Posts>();
        public ICollection<Comments>? Comments { get; set; } = new List<Comments>();
        public ICollection<UserBans>? UserBans { get; set; } = new List<UserBans>();
        public ICollection<RefrashToken>? RefrashTokens { get; set; } = new List<RefrashToken>();
    }
}
