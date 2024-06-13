using FluentValidation;
using TODO_User.Domain.Shared;

namespace TODO_User.Application.Dto.Identity
{
    public class CreateUserRequestDTOValidator : AbstractValidator<CreateUserRequestDTO>
    {
        public CreateUserRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nombre es obligatorio.");    

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email es obligatorio.")
                .EmailAddress().WithMessage("No es un correo valido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Contraseña es obligatoria.")
                .MinimumLength(PasswordLoginConstant.REQUIRED_MIN_LENGTH).WithMessage($"La contraseña debe contener al menos {PasswordLoginConstant.REQUIRED_MIN_LENGTH} caracteres.")
                .MaximumLength(PasswordLoginConstant.REQUIRED_MAX_LENGTH).WithMessage($"La contraseña no debe sobrepasar los {PasswordLoginConstant.REQUIRED_MAX_LENGTH} caracteres.")
                .Matches(@"[A-Z]").When(_ => PasswordLoginConstant.REQUIRE_UPPER_CASE).WithMessage("La contraseña debe contener al menos una letra minuscula.")
                .Matches(@"[a-z]").When(_ => PasswordLoginConstant.REQUIRE_LOWER_CASE).WithMessage("La contraseña debe contener al menos una letra mayusucula.")
                .Matches(@"[0-9]").When(_ => PasswordLoginConstant.REQUIRE_DIGIT).WithMessage("La contraseña debe contener al menos un digito.")
                .Matches(@"[\W]").When(_ => PasswordLoginConstant.REQUIRE_NON_ALPHANUMERIC).WithMessage("La contraseña debe contener al menos un simbolo.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirmar contraseña es obligatoria.")
                .Equal(x => x.Password).WithMessage("No coiniciden las contraseñas.");
        }
    }
}
