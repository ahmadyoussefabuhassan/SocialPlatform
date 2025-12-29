using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialPlatform.Application.Interface;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Helper;
using SocialPlatform.Domain.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialPlatform.Infrastructure.JWTService
{
    public class TokenJWTService : ITokenJWTService
    {
        private readonly JWT _jwt;
        private readonly UserManager<Users> _userManager;
        private readonly ITokenJWTRepository _tokenJWTRepository;
        public TokenJWTService(IOptions<JWT> jwt , UserManager<Users> userManager , ITokenJWTRepository tokenJWTRepository)
            => (_jwt, _userManager , _tokenJWTRepository) = (jwt.Value, userManager, tokenJWTRepository);

        public async Task<string> GenerateTokenAsync(Users user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString())
                
            };
            var UserRole = await _userManager.GetRolesAsync(user);
            foreach (var Role in UserRole)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: credentials
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refrashToken = new RefrashToken
            {
                Token = jwtToken,
                UserId = user.Id,
                Expires = DateTime.Now.AddDays(_jwt.DurationInDays),
                CreatedAt = DateTime.Now
            };
            await _tokenJWTRepository.AddSaveToken(refrashToken);

            return jwtToken;
        }
    }
}
