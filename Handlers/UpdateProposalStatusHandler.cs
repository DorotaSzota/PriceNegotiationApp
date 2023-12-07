using MediatR;
using PriceNegotiationApp.Commands;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Handlers;

public class UpdateProposalStatusHandler : IRequestHandler<UpdateProposalStatusCommand, UpdateProposalStatusDto>
{
    private readonly IPriceNegotiationService _priceNegotiationService;

    public UpdateProposalStatusHandler(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }
    public Task<UpdateProposalStatusDto> Handle(UpdateProposalStatusCommand request, CancellationToken cancellationToken)
    {
        _priceNegotiationService.UpdateProposalStatus(request.UpdateProposalStatusDto);
        return Task.FromResult(request.UpdateProposalStatusDto);
    }
}