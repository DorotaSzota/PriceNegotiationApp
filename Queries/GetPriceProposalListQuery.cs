using System.Net;
using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Queries;

public record GetPriceProposalListQuery : IRequest<List<GetPriceProposalDto>>;
