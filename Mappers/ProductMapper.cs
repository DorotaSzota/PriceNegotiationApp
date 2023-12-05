using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers
{
    public class ProductMapper
    {
        public static GetProductDto MapProductToGetProductDto(Product product)
        {
            return new GetProductDto
            {
                ProductName = product.ProductName,
                ProductCategory = product.ProductCategory,
                ProductDescription = product.ProductDescription,
                IsAvailable = product.IsAvailable
            };
        }
    }
}
