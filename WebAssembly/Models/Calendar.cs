using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class Calendar
{
    [Key]
    public int Id { get; set; }
    public required DateTime Date { get; set; }

    public required int CarId { get; set; }
    public virtual Car Car { get; set; }
}
