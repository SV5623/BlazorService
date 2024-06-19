using BlazorApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAssembly.Models;


namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;

        public CarController(UserManager<User> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var carsWithDetails = await _context.Cars
                .Include(c => c.CarDetails) // Включаємо деталі автомобілів
                .ToListAsync();
            return carsWithDetails;
        }


        //     [HttpPut("addCars")]
        //     //[Authorize(Roles = "Client")]
        //     public async Task<IActionResult> AddCars(string Id ,[FromBody] List<CarCreateDTO> carCreateDtos)
        //     {
        //         // var client = await _context.Clients
        //         //     .Include(c => c.Cars)
        //         //     .FirstOrDefaultAsync(c => c.Id == Id);
        //         //
        //         // var isInRole = await _userManager.IsInRoleAsync(client, "Client");
        //         // if (!isInRole)
        //         // {
        //         //     return Forbid("You must be in role 'Client' to add cars.");
        //         // }
        //
        //         foreach (var carCreateDto in carCreateDtos)
        //         {
        //             var car = new Car
        //             {
        //                 Brand = carCreateDto.Brand,
        //                 GraduationYear = carCreateDto.GraduationYear,
        //                 Price = carCreateDto.Price,
        //                 Warranty = carCreateDto.Warranty,
        //                 ClientId = Id
        //             };
        //             client.Cars.Add(car);
        //         }
        //
        //         await _context.SaveChangesAsync();
        //
        //         return Ok(client.ToUserClientDto(client.Cars.Select(c => c.Brand).ToList(), new List<string> { "Client" }));
        //     }
        //
        [HttpPut("addCars")]
        public async Task<IActionResult> AddCars(string clientId, [FromBody] List<CarCreateDTO> carCreateDtos)
        {
            foreach (var carCreateDto in carCreateDtos)
            {
                var car = new Car
                {
                    Brand = carCreateDto.Brand,
                    GraduationYear = carCreateDto.GraduationYear,
                    Price = carCreateDto.Price,
                    Warranty = carCreateDto.Warranty,
                    ClientId = clientId
                };
                _context.Cars.Add(car);
            }

            await _context.SaveChangesAsync();

            return Ok("Cars added successfully.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCar(int id, [FromBody] CarCreateDTO carDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Знаходимо машину за її ідентифікатором
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound(); // Машина з таким ідентифікатором не знайдена
            }

            // Оновлюємо властивості машини на основі отриманих даних з DTO
            car.Brand = carDTO.Brand;
            car.GraduationYear = carDTO.GraduationYear;
            car.Price = carDTO.Price;
            car.Warranty = carDTO.Warranty;

            try
            {
                await _context.SaveChangesAsync(); // Зберігаємо зміни
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent(); // Успішний відгук без контенту
        }

        [HttpPatch("{carId}/addCarDetails")]
        public async Task<IActionResult> AddCarDetails(int carId, [FromBody] CarDetailsDTO carDetailDTO)
        {
            if (carDetailDTO == null)
            {
                return BadRequest("Car details data is missing."); // Перевірка на наявність даних
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Перевірка на валідність моделі
            }

            // Знаходимо машину за її ідентифікатором
            var car = await _context.Cars.Include(c => c.CarDetails).FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null)
            {
                return NotFound($"Car with ID {carId} not found."); // Машина з таким ідентифікатором не знайдена
            }

            // Створюємо новий CarDetail на основі отриманих даних з DTO
            var carDetail = new Car_Detail()
            {
                Title = carDetailDTO.Title,
                Price = carDetailDTO.Price,
                Code = carDetailDTO.Code,
                CarId = car.Id // Пов'язуємо деталь з відповідною машиною
            };

            // Додаємо новий CarDetail до машини
            car.CarDetails.Add(carDetail);

// Оновлюємо атрибут HasDetails на true, оскільки машина тепер має деталі
            car.HasDetails = true;

            await _context.SaveChangesAsync(); // Зберігаємо зміни

            try
            {
                await _context.SaveChangesAsync(); // Зберігаємо зміни
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Failed to save car details."); // Помилка під час збереження змін
            }

            return NoContent(); // Успішний відгук без контенту
        }

    }
}
