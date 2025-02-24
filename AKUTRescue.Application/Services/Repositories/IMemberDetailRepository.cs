using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;

namespace AKUTRescue.Application.Services.Repositories
{
    public interface IMemberDetailRepository : IAsyncRepository<MemberDetail, Guid>
    {
        Task<MemberDetail> GetByMemberIdAsync(Guid memberId);
    }
} 