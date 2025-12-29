using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SocialPlatform.Application.Dtos.HelperException;
using SocialPlatform.Application.Dtos.User;
using SocialPlatform.Application.Interface;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly ITokenJWTService _jwtService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenJWTRepository _tokenJWTRepository;
        public UserService(UserManager<Users> userManager, ITokenJWTService jwtService, IMapper mapper, ILogger<UserService> logger, ITokenJWTRepository tokenJWTRepository)
            => (_userManager, _jwtService, _mapper, _logger ,_tokenJWTRepository ) 
            = (userManager, jwtService, mapper, logger, tokenJWTRepository);

        public async Task<Result<UsersDto>> DeleteUserProfileAsync(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                 return new Result<UsersDto>(false, null, "User not found");
                if (user.Role.ToString() == "Admin")
                    return new Result<UsersDto>(false, null, "Cannot delete Admin user");
                var result = await _userManager.DeleteAsync(user);
                return new Result<UsersDto>(result.Succeeded, null, result.Succeeded ? "User deleted successfully" : "Failed to delete user");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during deleting user profile");
                return new Result<UsersDto>(false, null, "An error occurred while deleting the profile");
            }
        }

        public async Task<Result<UsersDto>> GetUserProfileAsync(int userId)
        {
            try
            {
                var user = await _userManager.Users.Include(u => u.Posts)
                    .ThenInclude(p => p.Comments)
                     .Include(u => u.Comments)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    return new Result<UsersDto>(false, null, "User not found");
                var userdto = _mapper.Map<UsersDto>(user);
                return new Result<UsersDto>(true, userdto);
            }
            catch (Exception)
            {
                _logger.LogError("Error occurred during getting user profile");
                return new Result<UsersDto>(false, null, "An error occurred while retrieving the profile");
            }
        }

        public async Task<Result<LoginResponseDto>> LoginAsyne(LoginDto dto)
        {
            try
            {
                Users? user = null;
                if (dto.Idntitfar.Contains("@"))
                {
                    user = await _userManager.FindByEmailAsync(dto.Idntitfar);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(dto.Idntitfar);
                }
                if (user == null)
                    return new Result<LoginResponseDto>(false, Error: "Invalid credentials (User not found)");
                bool IsPassowrd = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!IsPassowrd)
                    return new Result<LoginResponseDto>(false, Error: "Invalid credentials (Wrong password)");
                var token = await _jwtService.GenerateTokenAsync(user);
                var userdto = _mapper.Map<UsersDto>(user);
                return new Result<LoginResponseDto>(true, new LoginResponseDto(userdto, token));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred during user Loging");
                return new Result<LoginResponseDto>(false, null, "Erorr Database");
            }
        }

        public async Task<Result<bool>> LoguotAsync(string token)
        {
            await _tokenJWTRepository.DeleteToken(token);
            return new Result<bool>(true, true, "User logged out successfully");
        }

        public async Task<Result<UsersDto>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var register = _mapper.Map<Users>(dto);
                register.CreatedAt = DateTime.UtcNow;
                var result = await _userManager.CreateAsync(register, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return new Result<UsersDto>(false,null , errors);
                }
                var userdto = _mapper.Map<UsersDto>(register);
                return new Result<UsersDto>(true, userdto, "User registered successfully");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred during user registration");
                return new Result<UsersDto>(false, null, "An error occurred during registration");

            }
        }

        public async Task<Result<UsersDto>> UpdateUserProfileAsync(int userId, UpdateUserDto updateDto)
        {
            try
            {
                var updateuser = await _userManager.FindByIdAsync(userId.ToString());
                if (updateuser == null)
                    return new Result<UsersDto>(false, null, "User not found");
                 _mapper.Map(updateDto, updateuser);
                var result = await _userManager.UpdateAsync(updateuser);
                if (!result.Succeeded)
                {
                    return new Result<UsersDto>(false, null, "Failed to update user profile");
                }
                return new Result<UsersDto>(true, _mapper.Map<UsersDto>(updateuser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during updating user profile");
                return new Result<UsersDto>(false, null, "An error occurred while updating the profile");
            }
        }
    }
}
