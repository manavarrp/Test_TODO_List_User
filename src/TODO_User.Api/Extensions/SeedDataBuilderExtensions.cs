using Microsoft.AspNetCore.Identity;
using TODO_User.Domain.Entities.Identity;
using TODO_User.Infrastructure.Persistence.Seeds;
using TODO_User.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TODO_User.Api.Extensions
{
    public static class SeedDataBuilderExtensions
    {
        public static void MigrateDatabase(this WebApplication webApp)
        {
            using var scope = webApp.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            context.Database.Migrate();
      
            //Identity
            RoleSeeder.SeedData(context, roleManager).Wait();
        }
    }
}
