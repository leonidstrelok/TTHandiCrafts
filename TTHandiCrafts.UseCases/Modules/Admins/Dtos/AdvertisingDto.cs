using System;
using AutoMapper;
using TTHandiCrafts.Models.Models;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;
using TTHandiCrafts.UseCases.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Dtos
{
    public class AdvertisingDto : Profile
    {
        /// <summary>
        /// Наименование рекламы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фотография рекламы
        /// </summary>
        public BinaryData Image { get; set; }

        /// <summary>
        /// Крайний срок завершение рекламы
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Ссылка для перехода
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Описание рекламы
        /// </summary>
        public string Description { get; set; }

        public AdvertisingDto()
        {
            CreateMap<Advertising, AdvertisingDto>()
                .ReverseMap();
        }
    }
}