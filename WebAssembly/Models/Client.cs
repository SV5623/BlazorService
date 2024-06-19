using System.ComponentModel.DataAnnotations;

namespace WebAssembly.Models;

public class Client : User
{
    public virtual List<Car> Cars { get; set; }
}
