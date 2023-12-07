using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Data;

public class PriceNegotiationDbContext : DbContext
{
    //!!!IMPORTANT!!! For future migrations comment out the seeder fields and the constructor
    public PriceNegotiationDbContext(DbContextOptions<PriceNegotiationDbContext> options) : base(options)
    { }

    private readonly PriceNegotiationSeeder _seeder;

    //public PriceNegotiationDbContext(DbContextOptions<PriceNegotiationDbContext> options) : base(options)
    //{
    //    _seeder = new PriceNegotiationSeeder(this);
    //    _seeder.Seed();
    //}

    public DbSet<Product> Products => Set<Product>();
    public DbSet<PriceProposal> PriceProposals => Set<PriceProposal>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

        modelBuilder.Entity<Role>().Property(r => r.RoleName).IsRequired();

        modelBuilder.Entity<PriceProposal>().Property(p => p.ProposedPrice).HasColumnType("decimal(5,2)");
        modelBuilder.Entity<PriceProposal>().Property(p => p.Message).HasMaxLength(160);

        modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Product>().Property(p => p.ProductDescription).HasMaxLength(160);
        modelBuilder.Entity<Product>().Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(5,2)");

    }




}