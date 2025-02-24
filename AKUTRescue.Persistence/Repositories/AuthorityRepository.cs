using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;
using AKUTRescue.Persistence.Contexts;
using AKUTRescue.Core.Repositories.EfCore;

namespace AKUTRescue.Persistence.Repositories
{
    public class AuthorityRepository : EfRepositoryBase<Authority, AKUTRescueDbContext>, IAuthorityRepository
    {
        public AuthorityRepository(AKUTRescueDbContext context) : base(context)
        {
        }

        public async Task<IList<Authority>> GetSubAuthoritiesAsync(Guid authorityId)
        {
            return await Context.Authorities
                .Where(a => a.ParentAuthorityId == authorityId)
                .ToListAsync();
        }

        public async Task<Authority> GetWithMembersAsync(Guid authorityId)
        {
            return await Context.Authorities
                .Include(a => a.Members)
                .FirstOrDefaultAsync(a => a.Id == authorityId);
        }
    }
} 