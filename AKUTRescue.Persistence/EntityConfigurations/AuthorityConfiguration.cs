using AKUTRescue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKUTRescue.Persistence.EntityConfigurations
{
    public class AuthorityConfiguration : IEntityTypeConfiguration<Authority>
    {
        public void Configure(EntityTypeBuilder<Authority> builder)
        {
            builder.ToTable("Authorities");
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Level).IsRequired();
            
            builder.HasOne(a => a.ParentAuthority)
                   .WithMany(a => a.SubAuthorities)
                   .HasForeignKey(a => a.ParentAuthorityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 