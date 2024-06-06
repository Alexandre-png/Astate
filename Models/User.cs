namespace Astate.Models;
public class UserDto
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public IFormFile? ProfileImage { get; set; }
}
