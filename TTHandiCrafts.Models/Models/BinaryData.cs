using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.Products.Enums;

namespace TTHandiCrafts.Models.Models
{
    public class BinaryData : BaseEntity
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Категория продукта
        /// </summary>
        public CategoryType CategoryType { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        public string FileName { get; set; }
    }
}