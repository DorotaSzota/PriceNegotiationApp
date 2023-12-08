using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public void AddPriceProposal(PriceProposalDto priceProposal)
    {
        _logger.LogInformation($"Adding or updating price proposal {priceProposal}.");
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == priceProposal.ProductId)?? 
                      throw new NotFoundException("Product not found.");
        var howManyAttempts = _dbContext.PriceProposals.Count(p =>
            p.ProductId == priceProposal.ProductId && p.UserId == priceProposal.UserId);

        var newProposal = new PriceProposal()
        {
            ProductId = priceProposal.ProductId,
            ProductPrice = product.ProductPrice,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            ProposedPrice = priceProposal.ProposedPrice,
            Accepted = false,
            AttemptsLeft =2-howManyAttempts,
            Message = string.Empty,
            UserId=priceProposal.UserId
        };
        if (!newProposal.Accepted && newProposal.AttemptsLeft > 0 && priceProposal.ProposedPrice > 2 * newProposal.ProductPrice)
        {
            throw new BadRequestException(
                "The proposed price is more than twice the product price. The price proposal is rejected.");
        }

        if (!newProposal.Accepted && newProposal.AttemptsLeft <=3 && newProposal.AttemptsLeft >=0) 
        {
            newProposal.AttemptsLeft--;
            _dbContext.PriceProposals.Add(newProposal);
            _dbContext.SaveChanges();
            return;
        }
        
        throw new Exception("Something wend wrong.");

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

        if (dto.Accepted)
        {
            proposal.Accepted = true;
            proposal.Message = dto.Message;
            _dbContext.SaveChanges();
        }

        if (!dto.Accepted)
        {
            proposal.Accepted = false;
            proposal.Message = dto.Message;
            _dbContext.SaveChanges();
        }
    }

}