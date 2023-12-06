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
    private readonly ILogger<ProductCatalogueService> _logger; //check if needed

    public PriceNegotiationService(IMapper mapper, PriceNegotiationDbContext dbContext, ILogger<ProductCatalogueService> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    //an async version of the GetAllProducts method needs to be implemented for the MediatR handler (it will make the mathods async)
    public List<GetProductDto> GetAllProducts()
    {
        var products =  _dbContext.Products.ToList();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }

    public void AddPriceProposal(PriceProposalDto priceProposal, Product product) 
    {
        var proposal =  _mapper.Map<PriceProposal>(priceProposal);
        _dbContext.PriceProposals.Add(proposal);

        if (proposal.Accepted == false & proposal.AttemptsLeft>=0)
        {
            if (priceProposal.ProposedPrice1 == 2 * (product.ProductPrice))
            {
                throw new Exception("The proposed price is double the product price. The price proposal is rejected.");
                _dbContext.PriceProposals.Remove(proposal);
            }
            if (priceProposal.ProposedPrice1 > product.ProductPrice)
            {
                _dbContext.SaveChanges();
            }
            else if (priceProposal.ProposedPrice1 < product.ProductPrice)
            {
                throw new BadRequestException("The proposed price is lower than the product price.");
            }
        }
        else if (proposal.Accepted == true)
        {
            throw new Exception("The price proposal has already been accepted.");
        }
        else if (proposal.AttemptsLeft < 0)
        {
            throw new Exception("The price proposal on this item is unavailable.");
        }
        proposal.AttemptsLeft--;
    }

}