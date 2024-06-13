using System.ComponentModel.DataAnnotations;

namespace TODO_User.Application.Dto.Identity
{
    public class CreateUserRequestDTO
    {
     
        public string Name { get; set; }
     
        public string Email { get; set; }

  
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        
    }
}
