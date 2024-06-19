using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAssembly.Models;

public class MechaniсWorkType
{
    [Key]
    public int Id { get; set; }
    public int MechanicId { get; set; }
    public virtual Mechaniс Mechanic { get; set; }

    public int WorkTypeId { get; set; }
    
    [ForeignKey("WorkTypeId")]
    public virtual WorkType WorkType { get; set; }
}

