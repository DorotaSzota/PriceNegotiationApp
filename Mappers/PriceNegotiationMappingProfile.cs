using AutoMapper;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Mappers
{
    public class PriceNegotiationMappingProfile : Profile
    {
        public PriceNegotiationMappingProfile()
        {
            CreateMap<PriceProposal, PriceProposalDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<Product, GetProductDto>();

            CreateMap<Product, PriceProposal>()
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Product, PriceProposalDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Product, AddProductDto>().ReverseMap();

            CreateMap<PriceProposalDto, PriceProposal>()
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProposedPrice))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AttemptsLeft, opt => opt.MapFrom(src => src.AttemptsLeft))
                .ForMember(dest => dest.Accepted, opt => opt.MapFrom(src => src.Accepted)).ReverseMap();

            CreateMap<GetPriceProposalDto, PriceProposal>().ReverseMap();

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
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProductCategory, GetPriceProposalDto>().ReverseMap();

            CreateMap<ProductCategory, UpdateProposalStatusDto>().ReverseMap();
            CreateMap<UpdateProposalStatusDto, PriceProposal>().ReverseMap();

            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
        }
    }
}
