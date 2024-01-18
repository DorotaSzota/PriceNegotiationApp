using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController( IAccountService accountService)
    {
        _accountService = accountService;
        
    }
    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
    {
        _accountService.RegisterUser(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
    {
        string token = _accountService.GenerateJwt(dto);
        return Ok(token);
    }
}



