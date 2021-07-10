using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.DeletProduct
{
    /// <summary>
    /// Обработчик удаления изделия
    /// </summary>
    public class DeletProductCommandHandler : AsyncRequestHandler<DeletProductCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public DeletProductCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        protected override async Task Handle(DeletProductCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Set<Product>().FirstAsync(p => p.Id == request.Id);
            dbContext.Set<Product>().Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }
}