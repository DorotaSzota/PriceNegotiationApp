using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly PriceNegotiationDbContext _dbContext;
    public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
    {
        var serviceResponse = new ServiceResponse<List<GetProductDto>>();


    }
}