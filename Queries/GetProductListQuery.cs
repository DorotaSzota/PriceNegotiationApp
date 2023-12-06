using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Queries;

public class GetProductListQuery : IRequest<List<GetProductDto>>  //can be also used as public record GetProductListQuerry : IRequest<List<GetProductDto>> because it's immutable
{
    
}