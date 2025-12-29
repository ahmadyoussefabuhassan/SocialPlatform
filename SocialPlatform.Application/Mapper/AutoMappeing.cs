using AutoMapper;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Application.Dtos.User;
using SocialPlatform.Application.Dtos.Post;
using SocialPlatform.Application.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Application.Mapper
{
    public class AutoMappeing : Profile
    {
        public AutoMappeing()
        {
            // Mappings for RegisterDto to Users
            CreateMap<RegisterDto, Users>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            // Mappings for Users
            CreateMap<Users, UsersDto>()
              .ReverseMap();
            // Mappings for UpdateUserDto to Users
            CreateMap<UpdateUserDto, Users>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));          
            // Mappings for Posts
            CreateMap<Posts, PostsDto>()
                .ForMember(dtos => dtos.UserName, opt => opt.MapFrom(ent => ent.Users != null ? ent.Users.UserName : string.Empty))
                .ReverseMap();
            // Mappings CreatePost for Posts
            CreateMap<CreatePostsDto, Posts>()
                .ForMember(dtos => dtos.Title, opt => opt.MapFrom(ent => ent.Title))
                .ForMember(dtos => dtos.Content, opt => opt.MapFrom(ent => ent.Content))
                .ForMember(dtos => dtos.ImageUrl, opt => opt.MapFrom(ent => ent.ImageUrl));
            // Mappings for Comments
            CreateMap<Comments, CommentsDto>()
                .ForMember(dtos => dtos.Title, opt => opt.MapFrom(ent => ent.Posts != null ? ent.Posts.Title : string.Empty))
                .ForMember(dtos => dtos.UserName, opt => opt.MapFrom(ent => ent.Users != null ? ent.Users.UserName : string.Empty))
                .ReverseMap();

            // Mappings CreateComment for Comments
            CreateMap<CreateCommentsDto , Comments>()
                .ForMember(dtos => dtos.Text, opt => opt.MapFrom(ent => ent.Text))
                .ForMember(dtos => dtos.PostId , opt => opt.MapFrom(ent => ent.PostId));
            // Mappings UpdateComments for Comments
            CreateMap<UpdateCommentsDto, Comments>();
            // Mappings for UserBans
            CreateMap<UserBans, UserBansDto>()
                .ForMember(dtos => dtos.UserName, opt => opt.MapFrom(ent => ent.Users != null ? ent.Users.UserName : string.Empty))
                .ForMember(dtos => dtos.BannedByAdminName, opt => opt.MapFrom(ent => ent.BannedByAdmin != null ? ent.BannedByAdmin.UserName : string.Empty))
                .ReverseMap();

        }
    }
}
