using SocialPlatform.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.User
{
    public class UserBansDto : BaseEntity
    {
        public required string Reason { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int BannedByAdminId { get; set; }
        public string BannedByAdminName { get; set; } = string.Empty;
    }
}
