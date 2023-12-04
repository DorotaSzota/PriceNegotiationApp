using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCatalogueController : ControllerBase
{
    private readonly IProductCatalogueService _productCatalogueService;

    public ProductCatalogueController(IProductCatalogueService productCatalogueService)
    {
        _productCatalogueService = productCatalogueService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductDto>> GetSingle(int id)
    {
        var result = await _productCatalogueService.GetSingle(id);
        return result;
    }
    
    
}