using AutoMapper;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers;

public class PriceNegotiationMappingProfile : Profile
{
    public PriceNegotiationMappingProfile()
    {
        CreateMap<PriceProposal, PriceProposalDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
        CreateMap<Product, GetProductDto>();
        CreateMap<Product,PriceProposal>()
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice)).
            ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Product, PriceProposalDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Product, AddProductDto>();
        CreateMap<AddProductDto, Product>();
        CreateMap<PriceProposalDto, PriceProposal>()
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProposedPrice))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
           
        CreateMap<GetPriceProposalDto, PriceProposal>();
        CreateMap<PriceProposal, GetPriceProposalDto>();

        CreateMap<GetPriceProposalDto, PriceProposal>()
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProposedPrice))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription));

        CreateMap<Product, GetPriceProposalDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice));
        CreateMap<Product, UpdateProposalStatusDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice));
        CreateMap<ProductCategory, GetPriceProposalDto>();
        CreateMap<GetPriceProposalDto, ProductCategory>();

        CreateMap<ProductCategory, UpdateProposalStatusDto>();
        CreateMap<UpdateProposalStatusDto, ProductCategory>();
        CreateMap<UpdateProposalStatusDto, PriceProposal>();
        CreateMap<PriceProposal, UpdateProposalStatusDto>();


    }

}