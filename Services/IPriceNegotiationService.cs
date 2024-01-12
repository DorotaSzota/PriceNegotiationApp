using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IPriceNegotiationService
{
    Task<List<GetProductDto>> GetAllProducts();
    Task<PriceProposalDto> AddPriceProposal(PriceProposalDto priceProposal);
    Task<List<GetPriceProposalDto>> GetAllPriceProposals();
    Task<GetPriceProposalDto> GetPriceProposalById(int id);
    Task UpdateProposalStatus(UpdateProposalStatusDto dto);

}