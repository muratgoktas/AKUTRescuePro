using AKUTRescue.Application.Services.Repositories;

using System;
using System.Threading.Tasks;

namespace AKUTRescue.Application.Features.Members.Rules
{
    public class MemberBusinessRules
    {   
        private readonly IMemberRepository _memberRepository;

        public MemberBusinessRules(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task EmailCannotBeDuplicatedWhenInserted(string email)
        {
            var result = await _memberRepository.AnyAsync(m => m.Email == email);
            if (result)
                throw new BusinessException(Messages.Member.AlreadyExists);
        }

        public async Task MemberShouldExistWhenRequested(Guid id)
        {
            var result = await _memberRepository.GetByIdAsync(id);
            if (result == null)
                throw new BusinessException(Messages.Member.NotFound);
        }
    }
} 