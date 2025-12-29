using SocialPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Interface
{
    public interface IPostRepository : IRepository<Posts>
    {
        Task<IEnumerable<Posts>> GetPostsByContentAsync(string Content);
        Task<IEnumerable<Posts>> GetPostsByUserAsync(int userId);
    }
}
