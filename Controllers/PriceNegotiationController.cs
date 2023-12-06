using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Commands;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Queries;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceNegotiationController : ControllerBase
{
    private readonly IMediator _mediator;

    public PriceNegotiationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("BrowseProducts")]
    public async Task<ActionResult<List<GetProductDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetProductListQuery()));
    }

    [HttpPost("AddPriceProposal")]
    public async Task<PriceProposalDto> AddPriceProposal([FromBody] PriceProposalDto priceProposal)
    {
        return await _mediator.Send(new AddPriceProposalCommand(priceProposal));
        
    }

    [HttpGet("GetAllPriceProposals")]
    public async Task<ActionResult<List<GetPriceProposalDto>>> GetAllPriceProposals()
    {
        return Ok(await _mediator.Send(new GetPriceProposalListQuery()));
    }   





}