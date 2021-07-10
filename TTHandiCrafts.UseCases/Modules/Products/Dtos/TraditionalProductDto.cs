using AutoMapper;
using TTHandiCrafts.Models.Models.Products;

namespace TTHandiCrafts.UseCases.Modules.Products.Dtos
{
    public class TraditionalProductDto : Profile
    {
        public TraditionalProductDto()
        {
            CreateMap<TraditionalProduct, TraditionalProductDto>()
                .ReverseMap();
        }
    }
}