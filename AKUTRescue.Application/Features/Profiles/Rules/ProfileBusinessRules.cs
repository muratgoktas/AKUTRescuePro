using System;
using System.Threading.Tasks;

using AKUTRescue.Application.Services;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;


namespace AKUTRescue.Application.Features.Profiles.Rules
{
    public class ProfileBusinessRules
    {
        private readonly IMemberRepository _memberRepository;

        public ProfileBusinessRules(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task EmailCannotBeDuplicatedWhenInserted(string email)
        {
            var result = await _memberRepository.AnyAsync(m => m.Email == email);
            if (result)
                throw new BusinessException("Bu e-posta adresi ile kayıtlı profil bulunmaktadır.");
        }

        public async Task ProfileShouldExistWhenRequested(Guid id)
        {
            var result = await _memberRepository.GetByIdAsync(id);
            if (result == null)
                throw new BusinessException("Profil bulunamadı.");
        }

        public async Task IdentityNumberCannotBeDuplicatedWhenInserted(string identityNumber)
        {
            var result = await _memberRepository.AnyAsync(
                m => m.MemberDetail.IdentityNumber == identityNumber);
            if (result)
                throw new BusinessException("Bu kimlik numarası ile kayıtlı profil bulunmaktadır.");
        }
    }
} 