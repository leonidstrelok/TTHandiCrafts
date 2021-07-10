using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.Products;

namespace TTHandiCrafts.Models.Models
{
    /// <summary>
    /// Работа рукодельниц или ремесленников
    /// </summary>
    public class UserWork : BaseEntity
    {
        /// <summary>
        /// Оценка изделия
        /// </summary>
        public double Evaluation { get; set; }
        /// <summary>
        /// Изделия
        /// </summary>
        public ICollection<Product> Products { get; set; }
        
    }
}