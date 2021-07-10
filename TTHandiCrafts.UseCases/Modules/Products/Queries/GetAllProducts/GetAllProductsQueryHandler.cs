using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(dbContext.Set<Product>().Where(p => p.ProductType == request.ProductType && p.CategoryType == request.CategoryType ).ProjectTo<ProductDto>(mapper.ConfigurationProvider));
        }
    }
}