namespace PriceNegotiationApp.Models;

public class GetProductDto
{
    public string ProductName { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public string ProductDescription { get; set; }
    public bool IsAvailable { get; set; } = true;

}