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

    [HttpGet("GetPriceProposalById/{id}")]
    public async Task<ActionResult<GetPriceProposalDto>> GetPriceProposalById(int id)
    {
        return Ok(await _mediator.Send(new GetPriceProposalByIdQuery(id)));
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