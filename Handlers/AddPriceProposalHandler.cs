using MediatR;
using PriceNegotiationApp.Commands;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Handlers;

public class AddPriceProposalHandler : IRequestHandler<AddPriceProposalCommand, PriceProposalDto>
{
    private readonly IPriceNegotiationService _priceNegotiationService;

    public AddPriceProposalHandler(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }

    public async Task<PriceProposalDto> Handle(AddPriceProposalCommand request, CancellationToken cancellationToken)
    {
        return await _priceNegotiationService.AddPriceProposal(request.PriceProposalDto);
    }


}