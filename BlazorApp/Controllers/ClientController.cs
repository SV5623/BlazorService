using BlazorApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAssembly.Mapper;
using WebAssembly.Models;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public ClientController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ClientGetAll()
        {
            var clients = await _userManager.GetUsersInRoleAsync("Client");
            var clientDtos = clients.Select(cl => cl.ToUserDto());
            return Ok(clientDtos);
        }
        // [HttpPost]
        // public async Task<IActionResult> CreateCar([FromBody] CarCreateDTO carCreateDto)
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     var user = await _userManager.FindByIdAsync(userId);
        //
        //     var isInRole = await _userManager.IsInRoleAsync(user, "Client");
        //     if (!isInRole)
        //     {
        //         return Forbid("You must be in role 'Client' to create a car.");
        //     }
        //
        //     var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == user.Id);
        //     if (client == null)
        //     {
        //         return NotFound("Client not found.");
        //     }
        //
        //     var car = new Car
        //     {
        //         Brand = carCreateDto.Brand,
        //         GraduationYear = carCreateDto.GraduationYear,
        //         Price = carCreateDto.Price,
        //         Warranty = carCreateDto.Warranty,
        //         ClientId = client.Id // Використання int
        //     };
        //
        //     _context.Cars.Add(car);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction(nameof(CreateCar), new { id = car.Id }, car);
        // }
    }
}