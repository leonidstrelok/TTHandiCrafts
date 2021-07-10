namespace TTHandiCrafts.Infrastructure.Interfaces.Interfaces
{
    /// <summary>
    /// Служба локализации
    /// </summary>
    public interface ILocalizationService
    {
        string this[string key] { get; }
        string StatusMustBeActive { get; }
        string ValueAlreadyExists { get; }
        string CascadeDependencyError { get; }
        string GetResource(string key);
    }
}