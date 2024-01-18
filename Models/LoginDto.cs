using System.ComponentModel.DataAnnotations;

namespace PriceNegotiationApp.Models;

public class LoginDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [MinLength(8)]
    [Required]
    public string Password { get; set; }

}