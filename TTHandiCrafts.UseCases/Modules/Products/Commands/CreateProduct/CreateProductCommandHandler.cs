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
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

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
            product = CastingToType(request);

            await LoadDocument(request, product);
            
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
        }

       

        private async Task LoadDocument(CreateProductCommand request, Product product)
        {
            if (request.Images != null)
            {
                var binarysData = new List<Product>();
                foreach (var versionFile in request.Images)
                {
                    Stream document = versionFile.Stream;
                    document.Position = 0;

                    using var ms = new MemoryStream();
                    await document.CopyToAsync(ms);
                    
                    product = CastingToType(request);
                    
                    binarysData.Add(product
                    {
                        Image = ms.ToArray(),
                        FileName = versionFile.FileName,
                        DocumentType = binaryData.DocumentType
                    });
                }

                await dbContext.Set<BinaryData>().AddRangeAsync(binarysData);
                await dbContext.SaveChangesAsync();
            }
        }
        
        private Product CastingToType(CreateProductCommand request)
        {
            Product product;
            if (request.CategoryType == CategoryType.Traditional)
            {
                product = mapper.Map<TraditionalProduct>(request);
            }
            else
            {
                product = mapper.Map<ModernProduct>(request);
            }

            return product;
        }
    }
}