using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AKUTRescue.Domain.Entities;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
        builder.Property(w => w.Code).IsRequired().HasMaxLength(20);
        builder.Property(w => w.Address).IsRequired().HasMaxLength(500);
        builder.Property(w => w.Type).IsRequired();
        builder.Property(w => w.Capacity).IsRequired();
        
        builder.HasOne(w => w.Location)
               .WithMany()
               .HasForeignKey(w => w.LocationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.ResponsibleMember)
               .WithMany()
               .HasForeignKey(w => w.ResponsibleMemberId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(w => w.Items)
               .WithOne(wi => wi.Warehouse)
               .HasForeignKey(wi => wi.WarehouseId);
    }
} 