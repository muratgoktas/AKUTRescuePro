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
    public class MemberRepository : EfRepositoryBase<Member, AKUTRescueDbContext>, IMemberRepository
    {
        public MemberRepository(AKUTRescueDbContext context) : base(context)
        {
        }

        public async Task<IList<Member>> GetMembersByTeamAsync(Guid teamId)
        {
            return await Context.Members
                .Include(m => m.MemberDetail)
                .Where(m => m.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<IList<Member>> GetMembersByAuthorityAsync(Guid authorityId)
        {
            return await Context.Members
                .Include(m => m.MemberDetail)
                .Where(m => m.AuthorityId == authorityId)
                .ToListAsync();
        }
    }
} 