using SocialPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Interface
{
    public interface ITokenJWTService
    {
       Task<string> GenerateTokenAsync(Users user);
    }
}
