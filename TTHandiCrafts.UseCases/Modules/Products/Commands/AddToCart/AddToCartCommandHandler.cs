using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.UserModels;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, int>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;

        public AddToCartCommandHandler(IApplicationDbContext dbContext, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.currentUserService = currentUserService;
        }

        public async Task<int> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == request.ProductId);
            var user = await dbContext.Set<User>().FirstOrDefaultAsync(p => p.Id == request.Id);
            user.Basket = new Basket()
            {
                UserId = 1,
                Count = user.Basket.Products.Count,
            };
            user.Basket.Products.Add(product);

            await dbContext.SaveChangesAsync();
            return 0;
        }
    }
}