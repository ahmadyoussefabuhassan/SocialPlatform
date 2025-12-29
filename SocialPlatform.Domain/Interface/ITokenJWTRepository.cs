using SocialPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Interface
{
    public interface ITokenJWTRepository
    {
        Task AddSaveToken(RefrashToken token);
        Task<RefrashToken> GetToken(string token);
        Task DeleteToken(string token);
    }
}
