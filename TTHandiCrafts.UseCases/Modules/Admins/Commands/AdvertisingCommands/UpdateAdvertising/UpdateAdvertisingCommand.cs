using MediatR;
using TTHandiCrafts.UseCases.Dtos;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.UpdateAdvertising
{
    public class UpdateAdvertisingCommand : IRequest<AdvertisingDto>
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование рекламы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фотография рекламы
        /// </summary>
        public VersionFile Image { get; set; }

        /// <summary>
        /// Ссылка для перехода
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Описание рекламы
        /// </summary>
        public string Description { get; set; }
    }
}