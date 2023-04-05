using Microsoft.EntityFrameworkCore;
using SER.Domain.Entities;

namespace SER.DataAccess;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("FirstConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>()
            .HasMany(s => s.Products)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Products)
            .WithMany(p => p.Customers)
            .UsingEntity(j => j.ToTable("CustomerProduct"));

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Shops)
            .WithMany(s => s.Customers)
            .UsingEntity(j => j.ToTable("CustomerShop"));
    }
    //entities
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shop> Shops { get; set; }
}
