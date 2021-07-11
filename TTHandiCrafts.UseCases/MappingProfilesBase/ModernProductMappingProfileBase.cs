using AutoMapper;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Commands.UpdateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.MappingProfilesBase
{
    public class ModernProductMappingProfileBase : Profile
    {
        public ModernProductMappingProfileBase()
        {
            CreateMap<CreateProductCommand, ModernProduct>();
            CreateMap<UpdateProductCommand, ModernProduct>();
            
            CreateMap<Product, ProductDto>()
                .ReverseMap();
        }
    }
}