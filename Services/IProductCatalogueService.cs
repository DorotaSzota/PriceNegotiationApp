using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IProductCatalogueService
{
    Task<List<GetProductDto>> GetAllProducts();
    Task<GetProductDto> GetProductById(int id);
    Task<ServiceResponse<AddProductDto>> AddProduct(AddProductDto newProduct);
    Task<List<GetProductDto>> DeleteProduct(int id);
  
}