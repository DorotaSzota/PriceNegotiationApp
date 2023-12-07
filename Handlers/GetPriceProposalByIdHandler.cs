using MediatR;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Queries;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Handlers;

public class GetPriceProposalByIdHandler : IRequestHandler<GetPriceProposalByIdQuery, GetPriceProposalDto>
{
    private readonly IPriceNegotiationService _priceNegotiationService;

    public GetPriceProposalByIdHandler(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }

    public Task<GetPriceProposalDto> Handle(GetPriceProposalByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_priceNegotiationService.GetPriceProposalById(request.Id));
    }

}