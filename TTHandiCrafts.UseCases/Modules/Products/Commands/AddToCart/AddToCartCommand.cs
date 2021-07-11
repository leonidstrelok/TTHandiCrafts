using MediatR;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор изделия
        /// </summary>
        public int ProductId { get; set; }
        
    }
}