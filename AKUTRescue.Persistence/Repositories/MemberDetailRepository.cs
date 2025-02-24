using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;
using AKUTRescue.Persistence.Contexts;
using AKUTRescue.Core.Repositories.EfCore;

namespace AKUTRescue.Persistence.Repositories
{
    public class MemberDetailRepository : EfRepositoryBase<MemberDetail, AKUTRescueDbContext>, IMemberDetailRepository
    {
        public MemberDetailRepository(AKUTRescueDbContext context) : base(context)
        {
        }

        public async Task<MemberDetail> GetByMemberIdAsync(Guid memberId)
        {
            return await Context.MemberDetails
                .FirstOrDefaultAsync(md => md.MemberId == memberId);
        }
    }
} 