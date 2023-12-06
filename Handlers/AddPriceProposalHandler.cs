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

    public Task<PriceProposalDto> Handle(AddPriceProposalCommand request, CancellationToken cancellationToken)
    {
        _priceNegotiationService.AddPriceProposal(request.PriceProposalDto);
        return Task.FromResult(_priceNegotiationService.AddPriceProposal(request.PriceProposalDto));
    }

}