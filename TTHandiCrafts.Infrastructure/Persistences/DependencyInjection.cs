using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;

namespace TTHandiCrafts.Infrastructure.Persistences
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTTHandiCraftsPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AppDbContext>());

            return services;
        }
    }
}