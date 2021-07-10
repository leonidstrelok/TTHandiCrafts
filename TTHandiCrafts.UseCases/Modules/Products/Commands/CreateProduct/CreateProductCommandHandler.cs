using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.Products.Enums;
using TTHandiCrafts.UseCases.Commons.Extensions;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;
using TTHandiCrafts.Utils;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = default;
            if (request.CategoryType == CategoryType.Traditional)
            {
                product = mapper.Map<TraditionalProduct>(request);
            }
            else
            {
                product = mapper.Map<ModernProduct>(request);
            }

            product.CreatedDate = SystemTime.Now();

            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();

            await product.LoadDocumentsAsync(request, product.Id, dbContext);


            return mapper.Map<ProductDto>(product);
        }
    }
}