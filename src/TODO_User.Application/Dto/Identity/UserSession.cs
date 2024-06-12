namespace TODO_User.Application.Dto.Identity
{
    public record UserSession(Guid Guid, string Name, string Email, string Role);
}
