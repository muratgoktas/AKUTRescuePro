using AKUTRescue.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKUTRescue.Application.Services.Repositories
{
    public interface IAuthorityRepository : IAsyncRepository<Authority, Guid>
    {
        Task<IList<Authority>> GetSubAuthoritiesAsync(Guid authorityId);
        Task<Authority> GetWithMembersAsync(Guid authorityId);
    }
} 