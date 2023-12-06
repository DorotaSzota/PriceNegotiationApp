using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Queries;

public record GetProductListQuery : IRequest<List<GetProductDto>>;  //records are immutable
