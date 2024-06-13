using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TODO_User.Application.Interface;
using TODO_User.Application.Interface.Identity;
using TODO_User.Infrastructure.Persistence;
using TODO_User.Infrastructure.Persistence.Repository;

namespace TODO_User.Infrastructure.Extension
{
    public static class DataBuilderExtension
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IAccountApplication, AccountRepository>();
            services.AddScoped<IJobApplication, JobRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });


            return services;

        }
    }
}
