using Microsoft.EntityFrameworkCore;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class PriceNegotiationService : IPriceNegotiationService
{
    
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly ILogger<ProductCatalogueService> _logger; //check if needed

    public PriceNegotiationService(PriceNegotiationDbContext dbContext, ILogger<ProductCatalogueService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //an async version of the GetAllProducts method needs to be implemented for the MediatR handler (it will make the mathods async)
    public List<GetProductDto> GetAllProducts()
    {
        var products =  _dbContext.Products.ToList();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }
    public PriceProposalDto 

}