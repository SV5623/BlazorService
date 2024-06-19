using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class CarDetailsDTO
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    public string Code { get; set; }
}