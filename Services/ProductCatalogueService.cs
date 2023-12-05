using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly PriceNegotiationDbContext _dbContext;

    public ProductCatalogueService(PriceNegotiationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    public async Task<List<GetProductDto>> GetAllProducts()
    {
        
        var products = await _dbContext.Products.ToListAsync();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }
}