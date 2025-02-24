using System;
using System.Threading.Tasks;
using AKUTRescue.Application.Services;
using AKUTRescue.Domain.Entities;

using AKUTRescue.Application.Services.Repositories;

namespace AKUTRescue.Application.Features.Teams.Rules
{
    public class TeamBusinessRules
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMemberRepository _memberRepository;

        public TeamBusinessRules(ITeamRepository teamRepository, IMemberRepository memberRepository)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
        }

        public async Task TeamCodeCannotBeDuplicatedWhenInserted(string code)
        {
            var result = await _teamRepository.AnyAsync(t => t.Code == code);
            if (result)
                throw new BusinessException(Messages.Team.AlreadyExists);
        }

        public async Task TeamShouldExistWhenRequested(Guid id)
        {
            var result = await _teamRepository.GetByIdAsync(id);
            if (result == null)
                throw new BusinessException(Messages.Team.NotFound);
        }

        public async Task TeamShouldNotHaveActiveMembers(Guid teamId)
        {
            var hasActiveMembers = await _memberRepository.AnyAsync(m => m.TeamId == teamId && m.Status);
            if (hasActiveMembers)
                throw new BusinessException("Aktif üyeleri olan ekip silinemez.");
        }
    }
} 