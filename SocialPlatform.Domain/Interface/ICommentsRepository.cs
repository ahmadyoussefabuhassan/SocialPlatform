using SocialPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Interface
{
    public interface ICommentsRepository : IRepository<Comments>
    {
        Task<int> GetCountCommentsByPostAsync(int postId);
        Task<int> GetCuontCommentsByUserAsync(int userId);
        
    }
}
