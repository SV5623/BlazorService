using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAssembly.Models;

namespace BlazorApp.Data;
public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    //
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // base.OnModelCreating(modelBuilder);
    //     // modelBuilder.Entity<User>().HasData(
    //     // new User {}
    //     // );
    // }
    public DbSet<User> Users { get; set; }

    public DbSet<Car_Detail> CarDetails { get; set; }
    public DbSet<Car> Cars { get; set; }

    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<MechaniсWorkType> MechaniсWorkTypes { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Mechaniс> Mechaniks { get; set; }

    public DbSet<WorkType> WorkTypes { get; set; }
}