using WebAssembly.Models;

namespace WebAssembly.Mapper;

public static class UserMappers
{
    public static UserDTO ToUserDto(this User userModel)
    {
        return new UserDTO()
        {
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email
        };
    }
}