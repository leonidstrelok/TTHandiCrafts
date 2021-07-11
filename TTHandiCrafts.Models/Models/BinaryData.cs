using System.Reflection.Metadata;
using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.Products.Enums;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;

namespace TTHandiCrafts.Models.Models
{
    /// <summary>
    /// Данные для хранения фотографий
    /// </summary>
    public class BinaryData : BaseEntity
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; set; }

        public int? ProductId { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public virtual Product Product { get; set; }

        public int? AdvertisingId { get; set; }

        /// <summary>
        /// Реклама
        /// </summary>
        public virtual Advertising Advertising { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        public string FileName { get; set; }
    }
}