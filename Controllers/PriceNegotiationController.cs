using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Commands;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Queries;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceNegotiationController : ControllerBase
{
    private readonly IPriceNegotiationService _priceNegotiationService;
    private readonly PriceNegotiationDbContext _dbContext;

    public PriceNegotiationController(IPriceNegotiationService priceNegotiationService, PriceNegotiationDbContext dbContext)
    {
        _priceNegotiationService = priceNegotiationService;
        _dbContext = dbContext;
    }

    [HttpGet("BrowseProducts")]
    public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
    {
        var serviceResponse = await _priceNegotiationService.GetAllProducts();
        return Ok(serviceResponse);
    }

    [HttpGet("GetPriceProposalById/{id}")]
    public async Task<ActionResult<GetPriceProposalDto>> GetPriceProposalById(int id)
    {
        var 
    }

    [HttpPost("AddPriceProposal")]
    public async Task<ActionResult<PriceProposalDto>> AddPriceProposal([FromBody] PriceProposalDto priceProposal)
    {
        return await _mediator.Send(new AddPriceProposalCommand(priceProposal));
        
    }

    [HttpGet("GetAllPriceProposals")]
    public async Task<ActionResult<List<GetPriceProposalDto>>> GetAllPriceProposals()
    {

        return Ok(await _mediator.Send(new GetPriceProposalListQuery()));
    }

    [HttpPut("UpdatePriceProposal")]
    public async Task<ActionResult> UpdatePriceProposal([FromBody] UpdateProposalStatusDto dto)
    {
        await _mediator.Send(new UpdateProposalStatusCommand(dto));
        return NoContent();
    }   





}