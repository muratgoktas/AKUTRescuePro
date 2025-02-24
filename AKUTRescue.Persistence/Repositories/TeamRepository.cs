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
    public class TeamRepository : EfRepositoryBase<Team, AKUTRescueDbContext>, ITeamRepository
    {
        public TeamRepository(AKUTRescueDbContext context) : base(context)
        {
        }

        public async Task<IList<Team>> GetSubTeamsAsync(Guid teamId)
        {
            return await Context.Teams
                .Where(t => t.ParentTeamId == teamId)
                .ToListAsync();
        }

        public async Task<Team> GetWithMembersAsync(Guid teamId)
        {
            return await Context.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamLeader)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }
    }
} 