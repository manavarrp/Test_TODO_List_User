using FluentValidation.Results;

namespace TODO_User.Application.Helpers
{
    public static class ValidationHelper
    {
        public static Dictionary<string, string> ConvertValidationErrorsToDictionary(ValidationResult validationResult)
        {
            var errors = new Dictionary<string, string>();
            foreach (var error in validationResult.Errors)
            {
                errors[error.PropertyName] = error.ErrorMessage;
            }
            return errors;
        }
    }
}
