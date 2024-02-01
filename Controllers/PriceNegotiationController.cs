using Microsoft.AspNetCore.Authorization;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;
using System.Security.Claims;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PriceNegotiationController : ControllerBase
{
    private readonly IPriceNegotiationService _priceNegotiationService;


    public PriceNegotiationController(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }

    [HttpGet("BrowseProducts")]
    public async Task<ActionResult<List<GetProductDto>>> GetAllProducts(ClaimsPrincipal user)
    {
        var serviceResponse = await _priceNegotiationService.GetAllProducts(user);
        return Ok(serviceResponse);
    }
    [HttpPost("AddPriceProposal")]
    public async Task<ActionResult<PriceProposalDto>> AddPriceProposal(PriceProposalDto priceProposal)
    {
        var serviceResponse = await _priceNegotiationService.AddPriceProposal(priceProposal);
        return Ok(serviceResponse);

    }

    [HttpGet("GetAllPriceProposals")]
    public async Task<ActionResult<List<GetPriceProposalDto>>> GetAllPriceProposals(RegisterUserDto dto)
    {

        var serviceResponse = await _priceNegotiationService.GetAllPriceProposals(dto);
        return Ok(serviceResponse);
    }

    [HttpGet("GetPriceProposalById/{id}")]
    public async Task<ActionResult<GetPriceProposalDto>> GetPriceProposalById(int id, RegisterUserDto dto)
    {
        var serviceResponse = await _priceNegotiationService.GetPriceProposalById(id, dto);
        return Ok(serviceResponse);
    }

 

    [HttpPut("UpdatePriceProposal")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdatePriceProposal([FromBody] UpdateProposalStatusDto dto)
    {
        await _priceNegotiationService.UpdateProposalStatus(dto);
        return NoContent();
    }   





}