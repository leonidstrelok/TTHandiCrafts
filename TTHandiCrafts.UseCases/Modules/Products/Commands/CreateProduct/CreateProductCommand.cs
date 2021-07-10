using System.Collections.Generic;
using MediatR;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.Products.Enums;
using TTHandiCrafts.UseCases.Dtos;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDto>
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
        public ICollection<VersionFile> Images { get; set; }

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