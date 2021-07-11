using AutoMapper;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Commands.UpdateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.MappingProfilesBase
{
    public class TraditionalProductMappingProfileBase : Profile
    {
        public TraditionalProductMappingProfileBase()
        {
            CreateMap<CreateProductCommand, TraditionalProduct>();
            CreateMap<UpdateProductCommand, TraditionalProduct>();
            
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}