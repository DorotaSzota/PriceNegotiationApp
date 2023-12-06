using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IPriceNegotiationService
{
    List<GetProductDto> GetAllProducts();
    void AddPriceProposal(PriceProposalDto priceProposal, Product product);
}