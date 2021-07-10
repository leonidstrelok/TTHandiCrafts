using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Infrastructure.Localizations;
using TTHandiCrafts.Infrastructure.Persistences;
using TTHandiCrafts.Infrastructure.Security.Extensions;

namespace TTHandiCrafts.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTTHandiCraftsInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTTHandiCraftsIdentity(configuration)
                .AddTTHandiCraftsLocalization()
                .AddTTHandiCraftsPersistence(configuration)
                .AddTTHandiCraftsSecurity();

            return services;
        }
    }
}