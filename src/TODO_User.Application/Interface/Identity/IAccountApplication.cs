using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Dto.Identity;

namespace TODO_User.Application.Interface.Identity
{
    public interface IAccountApplication
    {
        Task<BaseResponse> CreateUserAsync(CreateUserRequestDTO request);

        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
