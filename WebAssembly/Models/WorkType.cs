using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class WorkType
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual List<MechaniсWorkType> MechanicWorkTypes { get; set; }
}
