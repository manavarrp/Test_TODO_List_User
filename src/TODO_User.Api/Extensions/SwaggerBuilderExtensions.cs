using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TODO_User.Api.Extensions
{
    /// <summary>
    /// Clase de extensión para configurar Swagger en la aplicación ASP.NET Core.
    /// </summary>
    public static class SwaggerBuilderExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Configurar la versión y la información general de la API
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TODO List API",
                    Description = "An ASP.NET Core Web API for managing TODO list items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Support Team",
                        Email = "support@example.com",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
                // Configurar la definición de seguridad para JWT (OAuth2)
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {

                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }
}
