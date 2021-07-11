using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.UserModels;

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
        public virtual ICollection<Product> Products { get; set; }

        public int UserId { get; set; }
    }
}