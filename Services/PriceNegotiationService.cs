using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class PriceNegotiationService : IPriceNegotiationService
{
    
    private readonly IMapper _mapper;
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly ILogger<PriceNegotiationService> _logger; //check if needed

    public PriceNegotiationService(IMapper mapper, PriceNegotiationDbContext dbContext, ILogger<PriceNegotiationService> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    //an async version of the GetAllProducts method needs to be implemented for the MediatR handler (it will make the methods async)
    public List<GetProductDto> GetAllProducts()
    {
        var products =  _dbContext.Products.ToList();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }

    public PriceProposalDto AddPriceProposal(PriceProposalDto priceProposal) 
    {
        var proposal =  _mapper.Map<PriceProposal>(priceProposal);
        _dbContext.PriceProposals.Add(proposal);

        if (proposal.Accepted == false && proposal.AttemptsLeft>=0)
        {
            if (priceProposal.ProposedPrice == 2 * (proposal.ProductPrice))
            {
                _dbContext.PriceProposals.Remove(proposal);
                throw new Exception("The proposed price is double the product price. The price proposal is rejected.");
            }
            if (priceProposal.ProposedPrice < proposal.ProductPrice)
            {
                throw new BadRequestException("The proposed price is lower than the product price.");
            }
            if (priceProposal.ProposedPrice > proposal.ProductPrice)
            {
                _dbContext.SaveChanges();
            }
            proposal.AttemptsLeft--;
        }
        
        return _mapper.Map<PriceProposalDto>(proposal);
    }

    public List<GetPriceProposalDto> GetAllPriceProposals()
    {
        var proposals = _dbContext.PriceProposals.ToList();
        var proposalsDto = _mapper.Map<List<GetPriceProposalDto>>(proposals);
        return _mapper.Map<List<GetPriceProposalDto>>(proposals);
    }   

}