using AutoMapper;
using LinksShare.Models;
using LinksShare.Models.Dto;

namespace LinksShare.Profiles
{
    public class LinkInfoProfile : Profile
    {
        public LinkInfoProfile()
        {
            CreateMap<CreateLinkDto, LinkInfo>()
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            CreateMap<EditLinkDto, LinkInfo>()
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
                .ReverseMap();
        }
    }
}
