using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TODO_User.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
