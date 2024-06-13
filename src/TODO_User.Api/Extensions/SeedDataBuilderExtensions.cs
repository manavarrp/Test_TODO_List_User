using Microsoft.AspNetCore.Identity;
using TODO_User.Domain.Entities.Identity;
using TODO_User.Infrastructure.Persistence.Seeds;
using TODO_User.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TODO_User.Api.Extensions
{
    /// <summary>
    /// Clase de extensión para migración de base de datos y sembrado de datos iniciales.
    /// </summary>
    public static class SeedDataBuilderExtensions
    {
        public static void MigrateDatabase(this WebApplication webApp)
        {
            // Crear un scope para la vida útil del servicio - contexto de base de datos
            using var scope = webApp.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            // Aplicar migraciones pendientes en la base de datos
            context.Database.Migrate();
      
            //Identity
            RoleSeeder.SeedData(context, roleManager).Wait();
        }
    }
}
