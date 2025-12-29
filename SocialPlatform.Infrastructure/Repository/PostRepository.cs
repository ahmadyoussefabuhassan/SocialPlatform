using Microsoft.EntityFrameworkCore;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Infrastructure.Repository
{
    public class PostRepository : Repository<Posts> , IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context) : base (context)
            => _context = context;
        public override async Task<IEnumerable<Posts>> GetAllAsync()
            => await _context.Posts
              .Include(c => c.Comments.OrderByDescending(c => c.CreatedAt))
             .Include(us => us.Users.IsBanned == false)
              .OrderByDescending(p => p.CreatedAt)
              .ToListAsync();

        public async Task<IEnumerable<Posts>> GetPostsByContentAsync(string Content)
            => await _context.Posts.Where(p => p.Content.Contains(Content)).Include(c => c.Comments)
            .Include(us => us.Users.IsBanned == false)
               .OrderByDescending(p => p.CreatedAt)
              .ToListAsync();

        public async Task<IEnumerable<Posts>> GetPostsByUserAsync(int userId)
            => await _context.Posts.Where(p => p.UserId == userId)
               .OrderByDescending(p => p.CreatedAt)
              .ToListAsync();
    }
}
