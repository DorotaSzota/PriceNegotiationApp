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

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<List<GetProductDto>>> GetAll()
    {
        var serviceResponse = await _productCatalogueService.GetAllProducts();
        return Ok(serviceResponse);
    }


    
    
}