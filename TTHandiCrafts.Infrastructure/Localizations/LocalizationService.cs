using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Infrastructure.Localizations.Resources;
using TTHandiCrafts.UseCases.Commons.Constants;

namespace TTHandiCrafts.Infrastructure.Localizations
{
    public class LocalizationService : ILocalizationService
    {
        public string this[string key] => GetResource(key);

        public string GetResource(string key)
        {
            return Messages.ResourceManager.GetString(key) ?? key;
        }

        public string StatusMustBeActive => GetResource(LocalizationKeys.SharedKeys.StatusMustBeActive);
        public string CascadeDependencyError => GetResource(LocalizationKeys.SharedKeys.CascadeDependencyError);
        public string ValueAlreadyExists => GetResource(LocalizationKeys.SharedKeys.ValueAlreadyExists);
    }
}