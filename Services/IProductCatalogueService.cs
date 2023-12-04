using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IProductCatalogueService
{
    Task<ActionResult<GetProductDto>> GetSingle(int id);
}