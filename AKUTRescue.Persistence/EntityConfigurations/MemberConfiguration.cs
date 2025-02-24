using AKUTRescue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKUTRescue.Persistence.EntityConfigurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.LastName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(100);
            
            builder.HasOne(m => m.Authority)
                   .WithMany(a => a.Members)
                   .HasForeignKey(m => m.AuthorityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Team)
                   .WithMany(t => t.Members)
                   .HasForeignKey(m => m.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.MemberDetail)
                   .WithOne(md => md.Member)
                   .HasForeignKey<MemberDetail>(md => md.MemberId);
        }
    }
} 