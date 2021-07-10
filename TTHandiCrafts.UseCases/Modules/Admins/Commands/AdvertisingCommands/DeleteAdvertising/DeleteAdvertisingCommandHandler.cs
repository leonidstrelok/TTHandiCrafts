using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.DeleteAdvertising
{
    /// <summary>
    /// Обработчик удаления рекламы
    /// </summary>
    public class DeleteAdvertisingCommandHandler : AsyncRequestHandler<DeleteAdvertisingCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public DeleteAdvertisingCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        protected override async Task Handle(DeleteAdvertisingCommand request, CancellationToken cancellationToken)
        {
            var advertising = await dbContext.Set<Advertising>().FirstAsync(p => p.Id == request.Id);
            dbContext.Set<Advertising>().Remove(advertising);
            await dbContext.SaveChangesAsync();
        }
    }
}