using MediatR;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.DeletProduct
{
    public class DeletProductCommand : IRequest
    {
        public int Id { get; }

        public DeletProductCommand(int id)
        {
            Id = id;
        }
    }
}