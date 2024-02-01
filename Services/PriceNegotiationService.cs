using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;
using System.Security.Claims;

namespace PriceNegotiationApp.Services
{
public interface IPriceNegotiationService
{
    Task<List<GetProductDto>> GetAllProducts(ClaimsPrincipal user);
    Task<List<GetPriceProposalDto>> GetAllPriceProposals(RegisterUserDto dto);
    Task<GetPriceProposalDto> GetPriceProposalById(int id, RegisterUserDto dto);
    Task<PriceProposalDto> AddPriceProposal(PriceProposalDto priceProposal);
    Task UpdateProposalStatus(UpdateProposalStatusDto dto);

}

public class PriceNegotiationService : IPriceNegotiationService
{

    private readonly IMapper _mapper;
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly ILogger<PriceNegotiationService> _logger;

    public PriceNegotiationService(IMapper mapper, PriceNegotiationDbContext dbContext,
        ILogger<PriceNegotiationService> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<GetProductDto>> GetAllProducts(ClaimsPrincipal user)
    {
        var isAdmin = user.IsInRole("Admin");
        var products = await _dbContext.Products.ToListAsync();
        var productDtos = _mapper.Map<List<GetProductDto>>(products);
        foreach (var productDto in productDtos)
        {
            if (isAdmin)
            {
                productDto.PriceVisible = true; 
            }
            else
            {
                productDto.ProductPrice = null; 
                productDto.PriceVisible = false;
            }
        }

        return productDtos;
        }

    public async Task<List<GetPriceProposalDto>> GetAllPriceProposals(RegisterUserDto dto)
    {
        var proposals = await _dbContext.PriceProposals.ToListAsync();
        if (dto.RoleId == 1)
        { return _mapper.Map<List<GetPriceProposalDto>>(proposals); }
        if (dto.RoleId == 2)
        {
            var proposals2 = await _dbContext.PriceProposals.Where(p => p.UserId == dto.Id).ToListAsync();
            return _mapper.Map<List<GetPriceProposalDto>>(proposals2);
        }
        else
        {
            throw new UnauthorizedException("You are not authorized to view this page.");
        }
        
    }

    public async Task<GetPriceProposalDto> GetPriceProposalById(int id, RegisterUserDto dto)
    {
        var proposal = await _dbContext.PriceProposals.FindAsync(id);
        if (proposal is null)
        {
            throw new NotFoundException("Proposal id not found.");
        }

        if (dto.RoleId == 1)
        {
            return _mapper.Map<GetPriceProposalDto>(proposal);
        }

        if (dto.RoleId == 2)
        {
            if (proposal.UserId == dto.Id)
            {
                return _mapper.Map<GetPriceProposalDto>(proposal);
            }
            else
            {
                throw new UnauthorizedException("You are not authorized to view this page.");
            }
        }
        else
        {
            throw new UnauthorizedException("You are not authorized to view this page.");
        }

    }

    public async Task<PriceProposalDto> AddPriceProposal(PriceProposalDto priceProposal)
    {
        _logger.LogInformation($"Adding or updating price proposal {priceProposal}.");

        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == priceProposal.ProductId) ??
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
            AttemptsLeft = 2 - howManyAttempts,
            Message = string.Empty,
            UserId = priceProposal.UserId
        };
        if (!newProposal.Accepted && newProposal.AttemptsLeft > 0 &&
            priceProposal.ProposedPrice > 2 * newProposal.ProductPrice)
        {
            throw new BadRequestException(
                "The proposed price is more than twice the product price. The price proposal is rejected.");
        }

        if (!newProposal.Accepted && newProposal.AttemptsLeft <= 3 && newProposal.AttemptsLeft >= 0)
        {
            newProposal.AttemptsLeft--;
            await _dbContext.PriceProposals.AddAsync(newProposal);
            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<PriceProposalDto>(newProposal);
    }

    public async Task UpdateProposalStatus(UpdateProposalStatusDto dto)
    {
        _logger.LogInformation($"Updated price proposal status {dto}.");

        var proposal = await _dbContext.PriceProposals.FindAsync(dto.Id);
        if (proposal is null)
        {
            throw new NotFoundException("Proposal id not found.");
        }

        /*if (dto.Accepted)
        {
            proposal.Accepted = true;
            proposal.Message = dto.Message;
            _dbContext.SaveChangesAsync();
        }

        if (!dto.Accepted)
        {
            proposal.Accepted = false;
            proposal.Message = dto.Message;
            _dbContext.SaveChangesAsync();
        }*/

        proposal.Accepted = dto.Accepted;
        proposal.Message = dto.Message;

        await _dbContext.SaveChangesAsync();
    }
}
}