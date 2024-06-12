using Microsoft.AspNetCore.Identity;
using TODO_User.Domain.Entities.Identity;

namespace TODO_User.Infrastructure.Persistence.Seeds
{
    public class RoleSeeder
    {
        public static async Task SeedData(IdentityContext context, RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("User"))
            {
                Role user = new()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                };
                await roleManager.CreateAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
