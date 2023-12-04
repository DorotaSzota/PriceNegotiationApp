using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Data;

public class PriceNegotiationDbContext : DbContext
{
    private readonly PriceNegotiationSeeder _seeder;

    public PriceNegotiationDbContext(DbContextOptions<PriceNegotiationDbContext> options) : base(options)
    {
        _seeder = new PriceNegotiationSeeder(this);
        _seeder.Seed();
    }
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasMaxLength(160);
        modelBuilder.Entity<Product>().Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(5,2)");
    }


}