using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class Mechaniс : User
{
    public string Specialization { get; set; }
    public int Hours { get; set; }

    public virtual List<MechaniсWorkType> MechanicWorkTypes { get; set; }
}
