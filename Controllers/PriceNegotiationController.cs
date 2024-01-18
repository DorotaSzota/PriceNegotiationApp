using Microsoft.AspNetCore.Authorization;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

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
    public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
    {
        var serviceResponse = await _priceNegotiationService.GetAllProducts();
        return Ok(serviceResponse);
    }
    [HttpPost("AddPriceProposal")]
    public async Task<ActionResult<PriceProposalDto>> AddPriceProposal([FromQuery] PriceProposalDto priceProposal)
    {
        var serviceResponse = await _priceNegotiationService.AddPriceProposal(priceProposal);
        return Ok(serviceResponse);

    }

    [HttpGet("GetAllPriceProposalsAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<GetPriceProposalDto>>> GetAllPriceProposalsAdmin()
    {

        var serviceResponse = await _priceNegotiationService.GetAllPriceProposalsAdmin();
        return Ok(serviceResponse);
    }

    [HttpGet("GetPriceProposalByIdAdmin/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetPriceProposalDto>> GetPriceProposalByIdAdmin(int id)
    {
        var serviceResponse = await _priceNegotiationService.GetPriceProposalByIdAdmin(id);
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