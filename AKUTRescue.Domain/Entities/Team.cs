
using AKUTRescue.Core.Repositories;

namespace AKUTRescue.Domain.Entities;


public class Team : Entity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public TeamType Type { get; set; }
        public Guid LocationId { get; set; }
        public Guid TeamLeaderId { get; set; }
        public Guid? ParentTeamId { get; set; }

        // Navigation properties
        public virtual Member TeamLeader { get; set; }
        public virtual Team ParentTeam { get; set; }
        public virtual ICollection<Team> SubTeams { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual Location Location { get; set; }
    }
