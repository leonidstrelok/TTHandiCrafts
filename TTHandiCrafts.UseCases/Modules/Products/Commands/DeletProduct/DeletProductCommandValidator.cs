using FluentValidation;

namespace TTHandiCrafts.UseCases.Modules.Products.Commands.DeletProduct
{
    /// <summary>
    /// Валидатор для удаления изделия
    /// </summary>
    public class DeletProductCommandValidator : AbstractValidator<DeletProductCommand>
    {
        public DeletProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull();
        }
    }
}