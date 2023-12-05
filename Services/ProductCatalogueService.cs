using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly PriceNegotiationDbContext _dbContext;
    public Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
    {
        throw new NotImplementedException();
    }
}