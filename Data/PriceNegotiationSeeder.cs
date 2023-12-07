using System.Runtime.CompilerServices;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Data;

public class PriceNegotiationSeeder
{
   
    private readonly PriceNegotiationDbContext _dbContext;
    public PriceNegotiationSeeder(PriceNegotiationDbContext dbContext)    
    {
        _dbContext = dbContext;
    }
    public  void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.PriceProposals.Any())
            {
                var priceProposals = GetPriceProposals();
                _dbContext.AddRange(priceProposals);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Products.Any())
            {
                var products = GetProducts();
                _dbContext.AddRange(products);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }

    }

    private IEnumerable<Role> GetRoles()
    {
        return new List<Role>()
        {
            new Role()
            {
                Id = 1,
                RoleName = "Admin"
            },
            new Role()
            {
                Id = 2,
                RoleName = "User"
            }
        };
    }
    private IEnumerable<PriceProposal> GetPriceProposals()
    {
        return new List<PriceProposal>()
        {
            new PriceProposal()
            {
                Id = 1,
                ProductId = 1,
                ProductName = "Omega Smartphone",
                ProductDescription = "This is not even its final form.",
                ProductPrice = 999.99m,
                ProposedPrice = 1199.99m,
                Accepted = false,
                AttemptsLeft = 2,
                Message = "The proposed price is higher than the product price."
            },
            new PriceProposal()
            {
                Id = 2,
                ProductId = 3,
                ProductName = "Dancer's Set",
                ProductDescription = "Armor worn by the Dancer of the Boreal Valley.",
                ProductPrice = 66666.66m,
                ProposedPrice = 20000.00m,
                Accepted = false,
                AttemptsLeft = 1,
                Message = "The proposed price is lower than the product price."
            }
        };
    }

    private IEnumerable<Product> GetProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = 1,
                ProductName = "Omega Smartphone",
                ProductCategory = ProductCategory.Electronics,
                ProductDescription = "This is not even its final form.",
                ProductPrice = 999.99m,
                IsAvailable = true
            },
            new Product()
            {
                Id = 2,
                ProductName = "Shovel Ultra Pro 9000",
                ProductCategory = ProductCategory.Garden,
                ProductDescription = "Best suited for a shovel knight.",
                ProductPrice = 549.99m,
                IsAvailable = true
            },
            new Product()
            {
                Id = 3,
                ProductName = "Dancer's Set",
                ProductCategory = ProductCategory.Clothing,
                ProductDescription = "Armor worn by the Dancer of the Boreal Valley.",
                ProductPrice = 66666.66m,
                IsAvailable = true
            },
            new Product()
            {
                Id = 4,
                ProductName = "Nice Lamp",
                ProductCategory = ProductCategory.Home,
                ProductDescription = "It will guide your way through the night. ...or maybe not.",
                ProductPrice = 13.99m,
                IsAvailable = true
            },
            new Product()
            {
                Id = 5,
                ProductName = "Bandana",
                ProductCategory = ProductCategory.Sports,
                ProductDescription =
                    "An excellent pick for hunting and stealth missions. And fighting Metal Gear.",
                ProductPrice = 999.99m,
                IsAvailable = true
            }

        };

    }
}
