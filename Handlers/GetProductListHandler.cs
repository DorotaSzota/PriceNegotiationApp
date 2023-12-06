using MediatR;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Queries;
using PriceNegotiationApp.Services;

namespace PriceNegotiationApp.Handlers;

public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<GetProductDto>>
{
    private readonly IPriceNegotiationService _priceNegotiationService;

    public GetProductListHandler(IPriceNegotiationService priceNegotiationService)
    {
        _priceNegotiationService = priceNegotiationService;
    }
    public Task<List<GetProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_priceNegotiationService.GetAllProducts());
    }
}