using CRMBOT.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMBOT.Database.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;

    public DatabaseContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=crm;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasAlternateKey(x => x.INN);
    }
}