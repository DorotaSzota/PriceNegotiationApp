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


    }
    public static GetProductDto MapProductToGetProductDto(Product product)
    {
        return new GetProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            ProductCategory = product.ProductCategory,
            ProductDescription = product.ProductDescription,
            IsAvailable = product.IsAvailable
        };
    }
    public static AddProductDto MapProductToAddProductDto(Product product)
    {
        return new AddProductDto
        {
            ProductName = product.ProductName,
            ProductCategory = product.ProductCategory,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            IsAvailable = product.IsAvailable
        };
    }

}