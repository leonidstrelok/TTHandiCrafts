using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.Products.Enums;

namespace TTHandiCrafts.Models.Models.Products
{
    public abstract class Product : BaseEntity
    {
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Нужно ли изготовить 
        /// </summary>
        public bool ToMake { get; set; }

        /// <summary>
        /// Тип категорий
        /// </summary>
        public CategoryType CategoryType { get; set; }

        /// <summary>
        /// Фотография изделия
        /// </summary>
        public byte[]? Image { get; set; }

        /// <summary>
        /// Комментарий для изделия
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Тип изделия
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}