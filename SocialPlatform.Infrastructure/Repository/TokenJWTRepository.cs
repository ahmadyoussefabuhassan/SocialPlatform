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
    public class TokenJWTRepository : ITokenJWTRepository
    {
        private readonly ApplicationDbContext _context;
        public TokenJWTRepository(ApplicationDbContext context)
            => _context = context;
        public async Task AddSaveToken(RefrashToken token)
        {
            await _context.RefrashTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteToken(string token)
        {
            var t = await _context.RefrashTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (t != null)
            {
                _context.RefrashTokens.Remove(t);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<RefrashToken> GetToken(string token)
            => await _context.RefrashTokens.Include(token => token.Users)
                .FirstOrDefaultAsync(t => t.Token == token);
    }
}
