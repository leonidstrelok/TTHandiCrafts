using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.SharedEnums;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public UpdateProductCommandValidator(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(VerifyProduct);

            RuleFor(p => p.Name)
                .NotNull();
        }

        private async Task<bool> VerifyProduct(int id, CancellationToken arg2)
        {
            return await dbContext.Set<Product>().AnyAsync(p =>
                p.Id == id && p.MemberId == currentUserService.UserId && p.Member.Role == Role.Worker || p.Member.Role == Role.Admin);
        }
    }
}