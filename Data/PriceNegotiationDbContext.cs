using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Data;

public class PriceNegotiationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public string DbPath { get; }

    public PriceNegotiationDbContext()
    {
        DbPath = "PriceNegotiationDb.db";
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasMaxLength(160);
        modelBuilder.Entity<Product>().Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(5,2)");
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }   
}