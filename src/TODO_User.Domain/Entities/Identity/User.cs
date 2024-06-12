using Microsoft.AspNetCore.Identity;

namespace TODO_User.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}
