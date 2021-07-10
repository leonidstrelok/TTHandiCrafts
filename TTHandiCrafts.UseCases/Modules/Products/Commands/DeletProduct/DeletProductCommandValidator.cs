using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.DeletProduct
{
    /// <summary>
    /// Валидатор для удаления изделия
    /// </summary>
    public class DeletProductCommandValidator : AbstractValidator<DeletProductCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public DeletProductCommandValidator(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(VerifyProduct);
        }

        private async Task<bool> VerifyProduct(int id, CancellationToken arg2)
        {
            return await dbContext.Set<Product>().AnyAsync(p => p.Id == id && p.MemberId == currentUserService.UserId);
        }
    }
}