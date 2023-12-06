using MediatR;
using Microsoft.AspNetCore.Mvc;
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




    //[HttpPost]
    //public async Task<ActionResult> Post(AddProductDto addProductDto)
    //{
    //    await _mediator.Send(new AddProductCommand(addProductDto));
    //    return Ok();
    //}

   


    }