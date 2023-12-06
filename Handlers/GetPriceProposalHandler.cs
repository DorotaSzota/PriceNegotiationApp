using MediatR;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Queries;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Handlers;

public class GetPriceProposalHandler : IRequestHandler<GetPriceProposalListQuery, List<GetPriceProposalDto>>
{
    private readonly IPriceNegotiationService _priceNegotiationService;

    public GetPriceProposalHandler(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }

    public Task<List<GetPriceProposalDto>> Handle(GetPriceProposalListQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_priceNegotiationService.GetAllPriceProposals());
    }   
    
}