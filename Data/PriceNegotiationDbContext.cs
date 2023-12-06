using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Data;

public class PriceNegotiationDbContext : DbContext
{
    //!!!IMPORTANT!!! For future migrations comment out the seeder fields and the constructor


    private readonly PriceNegotiationSeeder _seeder;

    public PriceNegotiationDbContext(DbContextOptions<PriceNegotiationDbContext> options) : base(options)
    {
        _seeder = new PriceNegotiationSeeder(this);
        _seeder.Seed();
    }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<PriceProposal> PriceProposals => Set<PriceProposal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasMaxLength(160);
        modelBuilder.Entity<Product>().Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(5,2)");

        modelBuilder.Entity<PriceProposal>().Property(p => p.ProposedPrice1).HasColumnType("decimal(5,2)");
        modelBuilder.Entity<PriceProposal>().Property(p => p.ProposedPrice2).HasColumnType("decimal(5,2)");
        modelBuilder.Entity<PriceProposal>().Property(p => p.ProposedPrice3).HasColumnType("decimal(5,2)");
        modelBuilder.Entity<PriceProposal>().Property(p => p.Message).HasMaxLength(160);
    }

    


}