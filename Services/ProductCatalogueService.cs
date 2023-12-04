using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    public Task<ActionResult<GetProductDto>> GetSingle(int id)
    {
        throw new NotImplementedException();
    }
}