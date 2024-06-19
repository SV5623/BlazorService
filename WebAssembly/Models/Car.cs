using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class Car
{
    [Key]
    public int Id { get; set; }

    public required string Brand { get; set; }
    public string GraduationYear { get; set; }
    public decimal Price { get; set; }
    public string Warranty { get; set; }
    
    public virtual string ClientId { get; set; }

    public bool HasDetails { get; set; }
    public virtual ICollection<Car_Detail> CarDetails { get; set; }
}
