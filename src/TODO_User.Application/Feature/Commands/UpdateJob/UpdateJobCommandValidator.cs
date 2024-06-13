using FluentValidation;

namespace TODO_User.Application.Feature.Commands.UpdateJob
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Identificador es obligatorio.");
            RuleFor(x => x.State)
             .NotEmpty().WithMessage("Estado de la tarea es obligatorio.");
        }
    }
}
