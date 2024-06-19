
using WebAssembly.Models;

namespace WebAssembly.Mapper
{
    public static class UserClientMapper
    {
        public static UserClientDTO ToUserClientDto(this Client client, IList<string> cars, IList<string> roles)
        {
            return new UserClientDTO
            {
                Roles = new List<string>(roles),
                Cars = new List<string>(cars),
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email
            };
        }
    }
}