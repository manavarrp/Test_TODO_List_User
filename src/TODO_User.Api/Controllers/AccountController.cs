using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODO_User.Application.Dto.Identity;
using TODO_User.Application.Interface.Identity;

namespace TODO_User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApplication _accountApplication;

        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequestDTO userDTO)
        {

            var response = await _accountApplication.CreateUserAsync(userDTO);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _accountApplication.LoginAccount(loginDTO);
            return Ok(response);
        }

    }
}
