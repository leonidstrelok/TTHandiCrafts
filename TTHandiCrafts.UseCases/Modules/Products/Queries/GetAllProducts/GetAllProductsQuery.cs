using System.Collections.Generic;
using MediatR;
using TTHandiCrafts.Models.Models.Products.Enums;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public CategoryType CategoryType { get; }
        public ProductType ProductType { get; }

        public GetAllProductsQuery(CategoryType categoryType, ProductType productType)
        {
            CategoryType = categoryType;
            ProductType = productType;
        }
    }
}