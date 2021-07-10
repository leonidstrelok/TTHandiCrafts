using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == request.Id);
            return mapper.Map<ProductDto>(product);
        }
    }
}