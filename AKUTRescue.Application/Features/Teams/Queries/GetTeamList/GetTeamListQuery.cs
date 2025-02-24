using AKUTRescue.Application.Features.Teams.Queries.GetTeamList;
using AKUTRescue.Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AKUTRescue.Application.Features.Teams.Queries.GetTeamList
{
    public class GetTeamListQuery : IRequest<List<TeamListResponseDto>>
    {
        public bool? IsActive { get; set; }
        public TeamType? Type { get; set; }
        public Guid? LocationId { get; set; }
    }

    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public TeamType Type { get; set; }
        public string LeaderName { get; set; }
        public string Location { get; set; }
        public string ParentTeamName { get; set; }
    }

    public class GetTeamListQueryHandler : IRequestHandler<GetTeamListQuery, List<TeamListResponseDto>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamListQueryHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamListResponseDto>> Handle(GetTeamListQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetListAsync(
                predicate: t => (!request.IsActive.HasValue || t.Status == request.IsActive.Value) &&
                               (!request.Type.HasValue || t.Type == request.Type.Value) &&
                               (!request.LocationId.HasValue || t.LocationId == request.LocationId.Value),
                include: q => q
                    .Include(t => t.TeamLeader)
                    .Include(t => t.Location)
                    .Include(t => t.Members)
                    .Include(t => t.ParentTeam),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<List<TeamListResponseDto>>(teams.Items);
        }
    }
}