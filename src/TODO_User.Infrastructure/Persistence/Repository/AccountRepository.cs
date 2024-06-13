using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Dto.Identity;
using TODO_User.Application.Helpers;
using TODO_User.Application.Interface.Identity;
using TODO_User.Domain.Entities.Identity;

namespace TODO_User.Infrastructure.Persistence.Repository
{
    /// <summary>
    /// Repositorio que implementa la lógica de negocio para la gestión de cuentas de usuario.
    /// </summary>
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
            { // Verificar si ya existe un usuario con el mismo correo electrónico
                var existingUser = await _userManager.FindByEmailAsync(request.Email!);
                if (existingUser != null) return new BaseResponse(false, "Ya existe un usuario con ese correo");

                var newUser = new User()
                {
                    UserName = request.Email,
                    PasswordHash = request.Password,
                    Email = request.Email,
                    Name = request.Name,
                };
                // Validar la solicitud usando FluentValidation
                CreateUserRequestDTOValidator validator = new();
                ValidationResult validationResult = validator.Validate(request);

                var errors = ValidationHelper.ConvertValidationErrorsToDictionary(validationResult);

                var createUser = await _userManager.CreateAsync(newUser, request.Password);

                if (!createUser.Succeeded)
                {
                    return new BaseResponse(false, "No fue posible crear al usuario", errors);
                }
                // Asignar al nuevo usuario el rol de "User"
                await _userManager.AddToRoleAsync(newUser, "User");

                return new BaseResponse(true, "Cuenta Creada");
            }
            catch
            {
                return new BaseResponse(false, "Ocurrio un error al crear al usuario");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return new LoginResponse(false, null!, "Login container is empty");

                var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (getUser is null) return new LoginResponse(false, null!, "usuario no encontrado");

                bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
                if (!checkUserPasswords) return new LoginResponse(false, null!, "Usuario y/o password invalidos");

                // Obtener el rol del usuario
                var getUserRole = await _userManager.GetRolesAsync(getUser);
                // Crear una sesión de usuario con los datos necesarios
                var userSession = new UserSession(getUser.Id, getUser.Name!, getUser.Email!, getUserRole.First());
                string token = GenerateToken(userSession);
                return new LoginResponse(true, token, "Inicio de Session");

            }
            catch {
                return new LoginResponse(false, "","Ocurrio un error al iniciar la sesion");
            }
           
        }
        /// <summary>
        /// Método privado para generar un token JWT para la sesión de usuario.
        /// </summary>
        /// <param name="user">Datos de sesión del usuario.</param>
        /// <returns>Token JWT generado como cadena.</returns>
        private string GenerateToken(UserSession user)
        {
            // Configurar la clave de seguridad para firmar el token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Definir las claims del usuario para incluir en el token JWT
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
            // Crear y configurar el token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            // Escribir el token JWT como cadena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
