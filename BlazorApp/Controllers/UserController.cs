using System.Runtime.CompilerServices;
using System.Security.Claims;
using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAssembly.Mapper;
using WebAssembly.Models;

namespace BlazorApp.Controllers;

[Route("api/User")]
[ApiController]
// [Authorize(Role = "Admin")]
// [Authorize]
public class UserController : Controller
{
    private readonly DataContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UserController(DataContext context, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetInformation()
    {
        var roles = ((ClaimsIdentity)User.Identity)?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        var user = await _userManager.GetUserAsync(User);
        return Ok(new
        {
            User = user,
            Roles = roles
        });
    }
    //----------------------GET----------------------
    [HttpGet]
    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("DTO")]
    public async Task<IActionResult> GetAllUsersFull()
    {
        var users = await _context.Users.ToListAsync();
        var usersDtoWithRoles = new List<UserWithRoleDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userDtoWithRoles = UserWithRoleMappers.ToUserWithRoleDto(user, roles);
            usersDtoWithRoles.Add(userDtoWithRoles);
        }

        return Ok(usersDtoWithRoles);
    }

    //----------------------GET{id}----------------------
    [HttpGet("{id}")]
    public IActionResult GetUser([FromRoute] string id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // [HttpPatch("{email}")]
    // public IActionResult PatchUser([FromRoute] string email, [FromBody] UserDTO userDTO)
    // {
    //     var edit_user = _context.Users.FirstOrDefault(x => x.Email == email);
    //
    //     if (edit_user == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     edit_user.FirstName = userDTO.FirstName;
    //
    //     edit_user.LastName = userDTO.LastName;
    //
    //     edit_user.Email = userDTO.Email;
    //
    //     // Збереження змін
    //     _context.SaveChanges();
    //
    //     return Ok(edit_user.ToUserDto());
    // }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        // Виведення користувача із системи
        await _signInManager.SignOutAsync();

        // Видалення кукі
        Response.Cookies.Delete(".AspNetCore.Identity.Application");

        // Видалення користувача з бази даних
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser([FromRoute] string id, [FromBody] UserWithRoleDto userWithRole)
    {
        var userToUpdate = await _userManager.FindByIdAsync(id);

        if (userWithRole.Roles.Any())
        {
            if (userToUpdate != null)
            {
                var resultAddRoles = await _userManager.AddToRolesAsync(userToUpdate, userWithRole.Roles);
                if (!resultAddRoles.Succeeded)
                {
                    return BadRequest(resultAddRoles.Errors);
                }
            }
        }

        if (userToUpdate != null)
        {
            userToUpdate.FirstName = userWithRole.FirstName;
            userToUpdate.LastName = userWithRole.LastName;
            userToUpdate.Email = userWithRole.Email;

            var resultUpdate = await _userManager.UpdateAsync(userToUpdate);

            if (resultUpdate.Succeeded)
            {
                return Ok(userToUpdate.ToUserDto());
            }

            return BadRequest(resultUpdate.Errors);
        }
        return NotFound();
    }

}