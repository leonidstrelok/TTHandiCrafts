using AutoMapper;
using TTHandiCrafts.Models.Models.Products;

namespace TTHandiCrafts.UseCases.Modules.Products.Dtos
{
    public class ModernProductDto : Profile
    {
        public ModernProductDto()
        {
            CreateMap<ModernProduct, ModernProductDto>()
                .ReverseMap();
        }
    }
}