using MediatR;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Commands;

public record AddPriceProposalCommand(PriceProposalDto PriceProposalDto) : IRequest<PriceProposalDto>;