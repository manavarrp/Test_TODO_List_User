using FluentValidation;

namespace TODO_User.Application.Feature.Commands.CreateJob
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Nombre es obligatorio.");
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Descripcion es obligatoria.");
        }
    }
}
