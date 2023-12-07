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
    private readonly ILogger<PriceNegotiationService> _logger;

    public PriceNegotiationService(IMapper mapper, PriceNegotiationDbContext dbContext, ILogger<PriceNegotiationService> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public List<GetProductDto> GetAllProducts()
    {
        _mapper.Map<List<GetProductDto>>(_dbContext.Products.ToList());
        var products =  _dbContext.Products.ToList();
        return _mapper.Map<List<GetProductDto>>(products);
    }
    public GetPriceProposalDto GetPriceProposalById(int id){
        var proposal = _dbContext.PriceProposals.Find(id);
        if (proposal is null)
        {
            throw new NotFoundException("Proposal id not found.");
        }
        return _mapper.Map<GetPriceProposalDto>(proposal);
    }

    public async Task<PriceProposalDto> AddPriceProposal(PriceProposalDto priceProposal)
    {
        _logger.LogInformation($"Added new price proposal {priceProposal}.");

        var proposal = _mapper.Map<PriceProposal>(priceProposal);
        await _dbContext.PriceProposals.AddAsync(proposal);

        if (proposal.Accepted == false && proposal.AttemptsLeft >= 0)
        {
            if(priceProposal.ProposedPrice > proposal.ProductPrice)
            {
                await _dbContext.SaveChangesAsync();
            }
            if (priceProposal.ProposedPrice == 2 * (proposal.ProductPrice))
            {
                _dbContext.PriceProposals.Remove(proposal);
                throw new Exception("The proposed price is double the product price. The price proposal is rejected.");
            }
            if (priceProposal.ProposedPrice < proposal.ProductPrice)
            {
                throw new BadRequestException("The proposed price is lower than the product price.");
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

    public void UpdateProposalStatus(UpdateProposalStatusDto dto)
    {
        _logger.LogInformation($"Updated price proposal status {dto}.");
        var proposal = _dbContext.PriceProposals.Find(dto.Id);
        if (proposal is null)
        {
            throw new NotFoundException("Proposal id not found.");
        }

        if (dto.Accepted == true)
        {
            proposal.Accepted = true;
            _dbContext.SaveChanges();
        }

        if (dto.Accepted == false)
        {
            proposal.Accepted = false;
            _dbContext.SaveChanges();
        }
    }

}