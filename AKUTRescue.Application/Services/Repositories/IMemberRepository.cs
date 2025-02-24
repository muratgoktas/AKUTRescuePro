using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;

namespace AKUTRescue.Application.Services.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member, Guid>
    {
        Task<IList<Member>> GetMembersByTeamAsync(Guid teamId);
        Task<IList<Member>> GetMembersByAuthorityAsync(Guid authorityId);
    }
} 