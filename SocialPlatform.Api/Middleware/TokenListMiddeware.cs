using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using SocialPlatform.Application.Interface;
using SocialPlatform.Domain.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SocialPlatform.Api.Middleware
{
    public class TokenListMiddeware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public TokenListMiddeware(RequestDelegate next , IConfiguration configuration)
            => (_next,_configuration) = (next, configuration);
        public async Task InvokeAsync(HttpContext context , ITokenJWTRepository _tokenJWTRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(!string.IsNullOrEmpty(token))
            {
                var usertoken = await _tokenJWTRepository.GetToken(token);
                if( usertoken != null && usertoken.Expires > DateTime.UtcNow)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    SecurityToken validatedToken;
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                    if (principal != null)
                    {
                        context.User = principal;
                    }
                }

            }
            await _next(context);
        }
    }
}
