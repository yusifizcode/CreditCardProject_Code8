namespace TechnestHackhaton.Application.DTOs.User;

public class CreateUser
{
    public string UserName { get; set; } = null!;
    public string NameSurname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
