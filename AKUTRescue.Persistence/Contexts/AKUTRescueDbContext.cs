using Microsoft.EntityFrameworkCore;
using AKUTRescue.Domain.Entities;

namespace AKUTRescue.Persistence.Contexts
{
    public class AKUTRescueDbContext : DbContext
    {
        public AKUTRescueDbContext(DbContextOptions<AKUTRescueDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberDetail> MemberDetails { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseItem> WarehouseItems { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AKUTRescueDbContext).Assembly);
        }
    }
} 