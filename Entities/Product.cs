namespace PriceNegotiationApp.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductCategory { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
}