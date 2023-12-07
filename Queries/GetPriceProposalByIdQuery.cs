using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Queries;

public class GetPriceProposalByIdQuery : IRequest<GetPriceProposalDto>
{
    public int Id { get; }

    public GetPriceProposalByIdQuery(int id)
    {
        Id = id;
    }
}

