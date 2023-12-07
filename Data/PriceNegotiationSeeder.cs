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
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.AddRange(
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
                    });
                _dbContext.PriceProposals.AddRange(
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
                    );
                _dbContext.SaveChanges();
            }
        }
    }
}
