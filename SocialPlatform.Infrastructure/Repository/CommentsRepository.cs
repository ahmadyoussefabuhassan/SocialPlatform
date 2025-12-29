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
    public class CommentsRepository : Repository<Comments>, ICommentsRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentsRepository(ApplicationDbContext context) : base(context)
            => _context = context;
        public async Task<int> GetCountCommentsByPostAsync(int postId)
            => await _context.Comments.CountAsync(c => c.PostId == postId);

        public Task<int> GetCuontCommentsByUserAsync(int userId)
            => _context.Comments.CountAsync(c => c.UserId == userId);
    }
}
