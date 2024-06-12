using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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


            return services;

        }
    }
}
