using Microsoft.Extensions.DependencyInjection;

namespace TTHandiCrafts.Infrastructure.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTTHandiCraftsSecurity(this IServiceCollection services)
        {
            services.AddAuthorization(config => { config.AddSecurityPolicies(); });
            return services;
        }
    }
}