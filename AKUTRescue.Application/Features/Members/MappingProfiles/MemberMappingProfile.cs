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
                opt => opt.MapFrom(src => src.Authority.Name));

        CreateMap<IPaginate<Member>, IPaginate<MemberListResponseDto>>()
            .ForMember(dest => dest.Items,
                           opt => opt.MapFrom(src => src.Items.Select(x => new MemberListResponseDto
                           {
                               Id = x.Id,
                               FirstName = x.FirstName,
                               LastName = x.LastName,
                               FullName = $"{x.FirstName} {x.LastName}",
                               Email = x.Email,
                               PhoneNumber = x.ProfilePhotoUrl,
                               TeamId = x.TeamId,
                               TeamName = x.Team.Name,
                               AuthorityId = x.AuthorityId,
                               AuthorityName = x.Authority.Name,
                               CreateDate = x.CreateDate,
                               UpdateDate = x.UpdateDate,
                               Status = x.Status,
                               Barcode =x.Barcode
                           }).ToList()));
    }
}