using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.Infrastructure.Identities.Extensions;
using TTHandiCrafts.Infrastructure.Identities.Services;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;

namespace TTHandiCrafts.Infrastructure.Identities
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTTHandiCraftsIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TTHandiCraftsIdentityDbContext>((sp, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IIdentityService, IdentityService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters += " äşýňöüžçÄŞÝŇÖÜŽÇ";
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TTHandiCraftsIdentityDbContext>()
                .AddUserManager<ApplicationUserManager<IdentityUser>>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProfileService, DefaultProfileService>();
            services.AddIdentityServer4(configuration);

            return services;
        }
    }
}