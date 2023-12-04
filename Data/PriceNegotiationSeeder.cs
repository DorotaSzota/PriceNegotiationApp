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
                        Id=1,
                        ProductName = "Omega Smartphone",
                        ProductCategory = ProductCategory.Electronics,
                        ProductDescription = "This is not even its final form.",
                        ProductPrice = 999.99m,
                        IsAvailable = true
                    },
                new Product()
                {
                    Id=2,
                    ProductName = "Shovel Ultra Pro 9000",
                    ProductCategory = ProductCategory.Garden,
                    ProductDescription = "Best suited for a shovel knight.",
                    ProductPrice = 549.99m,
                    IsAvailable = true
                },
                new Product()
                {
                    Id=3,
                    ProductName = "Dancer's Set",
                    ProductCategory = ProductCategory.Clothing,
                    ProductDescription = "Armor worn by the Dancer of the Boreal Valley.",
                    ProductPrice = 66666.66m,
                    IsAvailable = true
                },
                new Product()
                {
                    Id=4,
                    ProductName = "Nice Lamp",
                    ProductCategory = ProductCategory.Home,
                    ProductDescription = "It will guide your way through the night. ...or maybe not.",
                    ProductPrice = 13.99m,
                    IsAvailable = true
                },
                new Product()
                    {
                        Id=5,
                        ProductName = "Bandana",
                        ProductCategory = ProductCategory.Sports,
                        ProductDescription = "An excellent pick for hunting and stealth missions. And fighting Metal Gear.",
                        ProductPrice = 999.99m,
                        IsAvailable = true
                    }
                    
                    
                    
                    );
                _dbContext.SaveChanges();
            }
        }
    }
}