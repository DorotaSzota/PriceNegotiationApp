using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public async Task<ServiceResponse<List<GetProductDto>>> Get()
    {
        return Ok(await _productCatalogueService.GetAllProducts);
    }


    
    
}