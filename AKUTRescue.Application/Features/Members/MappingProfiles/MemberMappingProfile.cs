using AutoMapper;


namespace AKUTRescue.Application.Features.Members.MappingProfiles;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<Member, MemberListResponseDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.TeamName,
                opt => opt.MapFrom(src => src.Team.Name))
            .ForMember(dest => dest.AuthorityName,
                opt => opt.MapFrom(src => src.Authority.Name))
            .ForMember(dest => dest.Barcode,
                opt => opt.MapFrom(src => src.Barcode));
                

        CreateMap<IPaginate<Member>, IPaginate<MemberListResponseDto>>()
            .ForMember(dest=>dest.Items,
            opt=>opt.MapFrom(src=>src.Items));
    }
}