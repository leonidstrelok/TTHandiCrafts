using FluentValidation;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.DeleteAdvertising
{
    public class DeleteAdvertisingCommandValidator : AbstractValidator<DeleteAdvertisingCommand>
    {
        public DeleteAdvertisingCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull();
        }
    }
}