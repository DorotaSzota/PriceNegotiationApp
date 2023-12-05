﻿using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;


namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<GetProductDto>> GetProductById([FromRoute]int id)
    {
        var serviceResponse = await _productCatalogueService.GetProductById(id);
        return Ok(serviceResponse);
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<GetProductDto>> AddProduct([FromBody]AddProductDto newProduct)
    {
        var serviceResponse = await _productCatalogueService.AddProduct(newProduct);
        if (serviceResponse.Success)
        {
            return Ok(serviceResponse);
        }
        return BadRequest(serviceResponse);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<ActionResult<GetProductDto>> DeleteProduct([FromRoute]int id)
    {
        var serviceResponse = await _productCatalogueService.DeleteProduct(id);
        if (serviceResponse.Success)
        {
            return Ok(serviceResponse);
        }
        return NotFound(serviceResponse);
    }



    
    
}