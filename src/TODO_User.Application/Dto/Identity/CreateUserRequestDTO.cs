using System.ComponentModel.DataAnnotations;

namespace TODO_User.Application.Dto.Identity
{
    public class CreateUserRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        
    }
}
