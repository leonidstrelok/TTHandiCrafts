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
            if (request.CategoryType == CategoryType.Traditional)
            {
                product = mapper.Map<TraditionalProduct>(request);
            }
            else
            {
                product = mapper.Map<ModernProduct>(request);
            }


            await LoadDocument(request);
            
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
        }

       

        private async Task LoadDocument(CreateProductCommand request)
        {
            if (request.Images != null)
            {
                var binarysData = new List<BinaryData>();
                foreach (var versionFile in request.Images)
                {
                    Stream document = versionFile.Stream;
                    document.Position = 0;

                    using var ms = new MemoryStream();
                    await document.CopyToAsync(ms);
                    
                    binarysData.Add(new BinaryData()
                    {
                        Image = ms.ToArray(),
                        FileName = versionFile.FileName,
                        CategoryType = request.CategoryType,
                        ProductType = request.ProductType
                    });
                }

                await dbContext.Set<BinaryData>().AddRangeAsync(binarysData);
                await dbContext.SaveChangesAsync();
            }
        }
        
    }
}