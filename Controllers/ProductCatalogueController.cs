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

    [HttpGet]
    public IEnumerable<GetProductDto> GetProduct()
    {
        var result = _productCatalogueService.GetProduct();
        return result;
    }
    
}