using TTHandiCrafts.Models.Models.BaseModels;

namespace TTHandiCrafts.Models.Models.UserModels.Advertisings
{
    /// <summary>
    /// Реклама
    /// </summary>
    public class Advertising : BaseEntity
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
        /// Ссылка для перехода
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Описание рекламы
        /// </summary>
        public string Description { get; set; }
    }
}