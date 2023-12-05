using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IProductCatalogueService
{
    Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
}