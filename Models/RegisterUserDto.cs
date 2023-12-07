using System.ComponentModel.DataAnnotations;

namespace PriceNegotiationApp.Models;

public class RegisterUserDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public int RoleId { get; set; } = 1;
}