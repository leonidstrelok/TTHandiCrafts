using MediatR;
using TTHandiCrafts.UseCases.Modules.Products.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>, IRequest<Unit>
    {
        public int Id { get; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}