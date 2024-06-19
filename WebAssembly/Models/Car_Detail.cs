using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class Car_Detail
{
    [Key]
    public int Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public required int CarId { get; set; }
    public string Code { get; set; }
}
