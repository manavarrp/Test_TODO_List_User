using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Dto.Identity;
using TODO_User.Application.Interface.Identity;
using TODO_User.Domain.Entities.Identity;

namespace TODO_User.Infrastructure.Persistence.Repository
{
    public class AccountRepository : IAccountApplication
    {
        private readonly UserManager<User> _userManager;
        IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
           _configuration = configuration;
        }

        public async Task<BaseResponse> CreateUserAsync(CreateUserRequestDTO request)
        {
          
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email!);
                if (existingUser != null) return new BaseResponse(false, "Ya existe un usuario con ese correo");

                var newUser = new User()
                {
                    UserName = request.Email,
                    PasswordHash = request.Password,
                    Email = request.Email,
                    Name = request.Name,
                };

              

                CreateUserRequestDTOValidator validator = new();
                ValidationResult validationResult = validator.Validate(request);


               var Errors = new ModelStateDictionary();
                foreach (var error in validationResult.Errors)
                {
                    Errors.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var createUser = await _userManager.CreateAsync(newUser, request.Password);

                if (!createUser.Succeeded)
                {
                   // var errors = createUser.Errors.Select(e => e.Description).ToList();
                    return new BaseResponse(false, "No posible crear al usuario", Errors);
                }


                await _userManager.AddToRoleAsync(newUser, "User");

                return new BaseResponse(true, "Cuenta Creada");
            }
            catch
            {
                return new BaseResponse(false, "No posible crear al usuario");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null) return new LoginResponse(false, null!, "usuario no encontrado");

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords) return new LoginResponse(false, null!, "Usuario y/o password invalidos");

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name!, getUser.Email!, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token, "Inicio de Session");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
