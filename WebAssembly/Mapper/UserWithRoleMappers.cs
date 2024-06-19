using WebAssembly.Models;

namespace WebAssembly.Mapper;

public static class UserWithRoleMappers
{
    public static UserWithRoleDto ToUserWithRoleDto(User userWithRoleModel, IList<string> roles)
    {
        return new UserWithRoleDto()
        {
            Roles = roles,
            FirstName = userWithRoleModel.FirstName,
            LastName = userWithRoleModel.LastName,
            Email = userWithRoleModel.Email
        };
    }
}