using Chat.Application.Behavior;
using Chat.Application.Contracts;
using Chat.Application.Helper;
using Chat.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Chat.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        


            // fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient( typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

           


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // JWT configuration
            services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

            services.AddScoped<IJwtService, JwtService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IRefreshTokenService,RefreshTokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // JWT authentication configuration
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                });

            services.AddAuthorization();

            return services;
        }

    }
}
