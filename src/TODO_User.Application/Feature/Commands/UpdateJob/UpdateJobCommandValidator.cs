using FluentValidation;

namespace TODO_User.Application.Feature.Commands.UpdateJob
{
    public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Identificador es obligatorio.");
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Nombre es obligatorio.");
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Descripcion es obligatoria.");
        }
    }
}
