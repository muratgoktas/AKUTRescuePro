using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AKUTRescue.Application.Services.Repositories;

namespace AKUTRescue.Application.Features.Members.Queries.GetMemberList
{
    public class GetMemberListQuery : IRequest<List<MemberListResponseDto>>
    {
        public bool? IsActive { get; set; }
        public Guid? TeamId { get; set; }
    }

    public class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, List<MemberListResponseDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberListQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<List<MemberListResponseDto>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetListAsync(
                predicate: m => (!request.IsActive.HasValue || m.Status == request.IsActive.Value) &&
                               (!request.TeamId.HasValue || m.TeamId == request.TeamId.Value),
                include: q => q.Include(m => m.Team).Include(m => m.Authority),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<List<MemberListResponseDto>>(members.Items);
        }
    }
} 