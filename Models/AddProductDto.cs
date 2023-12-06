using System.ComponentModel.DataAnnotations;

namespace PriceNegotiationApp.Models;

public class AddProductDto
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public ProductCategory ProductCategory { get; set; }
    [Required]
    public string ProductDescription { get; set; }
    [Required]
    public decimal ProductPrice { get; set; }
    [Required]
    public bool IsAvailable { get; set; } = true;
}