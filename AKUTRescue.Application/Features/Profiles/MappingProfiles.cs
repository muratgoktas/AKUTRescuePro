using AutoMapper;

using AKUTRescue.Domain.Entities;



namespace AKUTRescue.Application.Features.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Member mappings
            CreateMap<CreateMemberRequestDto, Member>();
            CreateMap<UpdateMemberRequestDto, Member>();
            
            CreateMap<Member, MemberResponseDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ForMember(dest => dest.AuthorityName, opt => opt.MapFrom(src => src.Authority.Name))
                .ForMember(dest => dest.FullName, opt => 
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Member, MemberListResponseDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ForMember(dest => dest.AuthorityName, opt => opt.MapFrom(src => src.Authority.Name))
                .ForMember(dest => dest.FullName, opt => 
                    opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Team mappings
            CreateMap<CreateTeamRequestDto, Team>();
            CreateMap<UpdateTeamRequestDto, Team>();
            CreateMap<Team, TeamResponseDto>()
                .ForMember(dest => dest.Location, opt => 
                    opt.MapFrom(src => $"{src.Location.City} - {src.Location.District}"))
                .ForMember(dest => dest.TeamLeaderFullName, opt => 
                    opt.MapFrom(src => $"{src.TeamLeader.FirstName} {src.TeamLeader.LastName}"))
                .ForMember(dest => dest.ParentTeamName, opt => 
                    opt.MapFrom(src => src.ParentTeam != null ? src.ParentTeam.Name : null))
                .ForMember(dest => dest.MemberCount, opt => 
                    opt.MapFrom(src => src.Members.Count));

            CreateMap<Team, TeamListResponseDto>()
                .ForMember(dest => dest.Location, opt => 
                    opt.MapFrom(src => $"{src.Location.City} - {src.Location.District}"))
                .ForMember(dest => dest.TeamLeaderFullName, opt => 
                    opt.MapFrom(src => $"{src.TeamLeader.FirstName} {src.TeamLeader.LastName}"))
                .ForMember(dest => dest.MemberCount, opt => 
                    opt.MapFrom(src => src.Members.Count));

            // Profile mappings
            CreateMap<CreateProfileRequestDto, Member>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));

            CreateMap<CreateProfileRequestDto, MemberDetail>();

            CreateMap<UpdateProfileRequestDto, Member>();
            CreateMap<UpdateProfileRequestDto, MemberDetail>();

            CreateMap<(Member Member, MemberDetail Detail), ProfileResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Member.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Member.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Member.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Member.LastName))
                .ForMember(dest => dest.FullName, opt => 
                    opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Member.Email))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Member.Team.Name))
                .ForMember(dest => dest.AuthorityName, opt => opt.MapFrom(src => src.Member.Authority.Name))
                .ForMember(dest => dest.MemberDetail, opt => opt.MapFrom(src => src.Detail));

            CreateMap<MemberDetail, MemberDetailResponseDto>();
        }
    }
} 