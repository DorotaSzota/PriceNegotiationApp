namespace PriceNegotiationApp.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public bool IsAvailable { get; set; } = true;
}