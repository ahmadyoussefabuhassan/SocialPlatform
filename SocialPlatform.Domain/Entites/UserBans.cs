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
    public class UserBans : BaseEntity
    {
        [MaxLength(255)]
        public required string Reason { get; set; }
        public int UserId { get; set; }
        public Users? Users { get; set; }
        public int BannedByAdminId { get; set; }
        public Users? BannedByAdmin { get; set; }
    }
}
