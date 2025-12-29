using SocialPlatform.Application.Dtos.HelperException;
using SocialPlatform.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Interface
{
    public interface IUserService
    {
        Task<Result<LoginResponseDto>> LoginAsyne(LoginDto dto);
        Task<Result<UsersDto>> RegisterAsync(RegisterDto dto);
        Task<Result<UsersDto>> GetUserProfileAsync(int userId);
        Task<Result<UsersDto>> UpdateUserProfileAsync(int userId, UpdateUserDto updateDto);
        Task<Result<bool>> LoguotAsync(string token);
        Task<Result<UsersDto>> DeleteUserProfileAsync(int userId);
    }
}
