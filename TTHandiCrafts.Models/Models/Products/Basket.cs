using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;

namespace TTHandiCrafts.Models.Models.Products
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Basket : BaseEntity
    {
        /// <summary>
        /// Количество изделий
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Продукты
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}