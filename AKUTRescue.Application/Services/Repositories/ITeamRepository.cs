using AKUTRescue.Domain.Entities;

namespace AKUTRescue.Application.Services.Repositories;

public interface ITeamRepository : IAsyncRepository<Team, Guid>
{
    Task<IList<Team>> GetSubTeamsAsync(Guid teamId);
    Task<Team> GetWithMembersAsync(Guid teamId);
}