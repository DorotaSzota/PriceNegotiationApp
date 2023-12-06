using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IPriceNegotiationService
{
    List<GetProductDto> GetAllProducts();
    PriceProposalDto AddPriceProposal(PriceProposalDto priceProposal);
    List<GetPriceProposalDto> GetAllPriceProposals();
}