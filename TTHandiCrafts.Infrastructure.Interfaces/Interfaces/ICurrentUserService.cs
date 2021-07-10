namespace TTHandiCrafts.Infrastructure.Interfaces.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        bool IsAdminRole { get; }
    }
}