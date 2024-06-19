namespace WebAssembly.Models;

public class UserClientDTO
{
    public IList<string> Roles { get; set; }
    public IList<string> Cars { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

