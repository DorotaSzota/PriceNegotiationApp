﻿using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("GetPriceProposalById/{id}")]
    public async Task<ActionResult<GetPriceProposalDto>> GetPriceProposalById(int id)
    {
        var serviceResponse = await _priceNegotiationService.GetPriceProposalById(id);
        return Ok(serviceResponse);
    }

    [HttpPost("AddPriceProposal")]
    public async Task<ActionResult<PriceProposalDto>> AddPriceProposal([FromBody] PriceProposalDto priceProposal)
    {
       var serviceResponse = await _priceNegotiationService.AddPriceProposal(priceProposal);
       return Ok(serviceResponse);
        
    }

    [HttpGet("GetAllPriceProposals")]
    public async Task<ActionResult<List<GetPriceProposalDto>>> GetAllPriceProposals()
    {

        var serviceResponse = await _priceNegotiationService.GetAllPriceProposals();
        return Ok(serviceResponse);
    }

    [HttpPut("UpdatePriceProposal")]
    public async Task<ActionResult> UpdatePriceProposal([FromBody] UpdateProposalStatusDto dto)
    {
        await _priceNegotiationService.UpdateProposalStatus(dto);
        return NoContent();
    }   





}