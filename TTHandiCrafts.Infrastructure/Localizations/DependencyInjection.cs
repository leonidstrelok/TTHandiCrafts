using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;

namespace TTHandiCrafts.Infrastructure.Localizations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTTHandiCraftsLocalization(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationService, LocalizationService>();
            ValidatorOptions.Global.LanguageManager = new TurkmenLanguageManager();

            return services;
        }
    }
}