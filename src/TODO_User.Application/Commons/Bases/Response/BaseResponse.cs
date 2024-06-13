using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TODO_User.Application.Commons.Bases.Response
{
    public record class BaseResponse(bool Flag, string Message, Dictionary<string, string>? Errors = null);
    public record class LoginResponse(bool Flag, string Token, string Message);
}
