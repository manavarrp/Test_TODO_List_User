using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TODO_User.Domain.Entities.Identity;
using TODO_User.Domain.Shared;
using TODO_User.Infrastructure.Persistence;

namespace TODO_User.Infrastructure.Extension
{
    /// <summary>
    /// Clase de extensión para configurar la identidad de usuario en ASP.NET Core Identity.
    /// </summary>
    public static class IdentityBuilderExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                // Configuración de requisitos de contraseña
                options.Password.RequireDigit = PasswordLoginConstant.REQUIRE_DIGIT;
                options.Password.RequireLowercase = PasswordLoginConstant.REQUIRE_LOWER_CASE;
                options.Password.RequireUppercase = PasswordLoginConstant.REQUIRE_UPPER_CASE;
                options.Password.RequireNonAlphanumeric = PasswordLoginConstant.REQUIRE_NON_ALPHANUMERIC;
                options.Password.RequiredLength = PasswordLoginConstant.REQUIRED_MIN_LENGTH;
                // Requerir correo electrónico único para cada usuario
                options.User.RequireUniqueEmail = true;
               

            })
           .AddRoles<Role>()
           .AddEntityFrameworkStores<IdentityContext>()
           .AddDefaultTokenProviders();// Habilitar la generación de tokens por defecto para la aplicación

            // Configuración del tiempo de vida del token de restablecimiento de contraseña
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(2));

        }
    
    }
}
