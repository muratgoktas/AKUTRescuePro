using AKUTRescue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AKUTRescue.Persistence.EntityConfigurations
{
    public class MemberDetailConfiguration : IEntityTypeConfiguration<MemberDetail>
    {
        public void Configure(EntityTypeBuilder<MemberDetail> builder)
        {
            builder.ToTable("MemberDetails");
            builder.HasKey(md => md.Id);
            
            builder.Property(md => md.IdentityNumber).IsRequired().HasMaxLength(20);
            builder.Property(md => md.PhoneNumber).IsRequired().HasMaxLength(20);
            
            builder.Property(md => md.Certifications)
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
            
            builder.Property(md => md.SpecialSkills)
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        }
    }
} 