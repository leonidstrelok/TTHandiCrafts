using MediatR;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.DeleteAdvertising
{
    /// <summary>
    /// Удаление рекламы
    /// </summary>
    public class DeleteAdvertisingCommand : IRequest
    {
        public int Id { get; }

        public DeleteAdvertisingCommand(int id)
        {
            Id = id;
        }
    }
}