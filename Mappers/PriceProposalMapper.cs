using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers;

public class PriceProposalMapper
{
    public static PriceProposalDto MapProductToPriceProposalDto(PriceProposal priceProposal, Product product)
    {
        return new PriceProposalDto
        {
            ProductId = product.Id,
            ProposedPrice1 = priceProposal.ProposedPrice1,
            ProposedPrice2 = priceProposal.ProposedPrice2,
            ProposedPrice3 = priceProposal.ProposedPrice3, 
            Accepted = priceProposal.Accepted,
            AttemptsLeft = priceProposal.AttemptsLeft,
            Message = priceProposal.Message
        };
    }
}