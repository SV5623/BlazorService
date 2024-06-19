namespace WebAssembly.Models;

public class UserWithRoleDto
{
    public IList<string> Roles { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

}