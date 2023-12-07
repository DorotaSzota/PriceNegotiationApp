using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Commands;

public record UpdateProposalStatusCommand(UpdateProposalStatusDto UpdateProposalStatusDto) : IRequest<UpdateProposalStatusDto>;
