using AutoMapper;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers;

public class PriceNegotiationMappingProfile : Profile
{
    public PriceNegotiationMappingProfile()
    {
        CreateMap<PriceProposal, PriceProposalDto>();
        CreateMap<Product, GetProductDto>();
        CreateMap<Product,PriceProposal>()
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice)).
            ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Product, PriceProposalDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

    }
}