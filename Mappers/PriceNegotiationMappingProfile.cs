using AutoMapper;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers;

public class PriceNegotiationMappingProfile : Profile
{
    public PriceNegotiationMappingProfile()
    {
        CreateMap<PriceProposal, PriceProposalDto>();
        CreateMap<Product, GetProductDto>();
    }
}