using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Dtos.User
{
    public record RegisterDto(
        string UserName,
        string Email,
        string Password
    );
    public record LoginDto(
        string Idntitfar,
        string Password
    );
    public record LoginResponseDto(
        UsersDto User,
        string? Token = null
    );
}
