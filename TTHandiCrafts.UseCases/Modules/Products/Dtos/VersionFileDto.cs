using AutoMapper;
using TTHandiCrafts.Models.Models;
using TTHandiCrafts.UseCases.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Dtos
{
    public class VersionFileDto : Profile
    {
        public VersionFileDto()
        {
            CreateMap<BinaryData, VersionFile>()
                .ReverseMap();
        }
    }
}