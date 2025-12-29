
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialPlatform.Api.Middleware;
using SocialPlatform.Application.Interface;
using SocialPlatform.Application.Mapper;
using SocialPlatform.Application.Services;
using SocialPlatform.Domain.Entites;
using SocialPlatform.Domain.Helper;
using SocialPlatform.Domain.Interface;
using SocialPlatform.Infrastructure;
using SocialPlatform.Infrastructure.JWTService;
using SocialPlatform.Infrastructure.Repository;

namespace SocialPlatform.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add JWT
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // Add DbContext 
            var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                if(string.IsNullOrEmpty(connStr))
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                options.UseSqlServer(connStr);
            });
            // Add Identity
            builder.Services.AddIdentity<Users, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var jwtSettings = builder.Configuration.GetSection("JWT").Get<JWT>();
                if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
                    throw new InvalidOperationException("JWT settings are not properly configured.");
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bazar API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "√œŒ· «· Êﬂ‰ »«·‘ﬂ· «· «·Ì: Bearer [space] token"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            // Add Repositories
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
            builder.Services.AddScoped<ITokenJWTRepository, TokenJWTRepository>();
            // Add Application Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICommentSrevice, CommentSrevice>();
            builder.Services.AddScoped<ITokenJWTService, TokenJWTService>();
            builder.Services.AddScoped<IPostService, PostService>();
            // Add AutoMapper
            builder.Services.AddAutoMapper(cf => cf.AddProfile<AutoMappeing>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            // Add Enable Middleware
            app.UseMiddleware<TokenListMiddeware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
