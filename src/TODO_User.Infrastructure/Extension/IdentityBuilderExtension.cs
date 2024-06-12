using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TODO_User.Domain.Entities.Identity;
using TODO_User.Domain.Shared;
using TODO_User.Infrastructure.Persistence;

namespace TODO_User.Infrastructure.Extension
{
    public static class IdentityBuilderExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                /*Password*/
                options.Password.RequireDigit = PasswordLoginConstant.REQUIRE_DIGIT;
                options.Password.RequireLowercase = PasswordLoginConstant.REQUIRE_LOWER_CASE;
                options.Password.RequireUppercase = PasswordLoginConstant.REQUIRE_UPPER_CASE;
                options.Password.RequireNonAlphanumeric = PasswordLoginConstant.REQUIRE_NON_ALPHANUMERIC;
                options.Password.RequiredLength = PasswordLoginConstant.REQUIRED_MIN_LENGTH;
                //options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.User.RequireUniqueEmail = true;
               

            })
           .AddRoles<Role>()
           .AddEntityFrameworkStores<IdentityContext>()
           .AddDefaultTokenProviders();//Enable token generation

            //Reset token is to be valid for a limited time, for example, 2 hours.
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(2));

        }
    
    }
}
