using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers;

public class PriceProposalMapper
{
    public static PriceProposalDto MapPriceProposalToPriceProposalDto(PriceProposal priceProposal, Product product)
    {
        return new PriceProposalDto
        {
            ProductId = product.Id,
            ProposedPrice = priceProposal.ProposedPrice,
            Accepted = priceProposal.Accepted,
            AttemptsLeft = priceProposal.AttemptsLeft,
            Message = priceProposal.Message
        };
    }
}