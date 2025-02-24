using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AKUTRescue.Domain.Entities;

namespace AKUTRescue.Persistence.EntityConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Code).IsRequired().HasMaxLength(20);
            
            builder.HasOne(t => t.TeamLeader)
                   .WithMany()
                   .HasForeignKey(t => t.TeamLeaderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.ParentTeam)
                   .WithMany(t => t.SubTeams)
                   .HasForeignKey(t => t.ParentTeamId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 