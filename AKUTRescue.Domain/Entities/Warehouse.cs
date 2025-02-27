using AKUTRescue.Core.Repositories;
using AKUTRescue.Domain.Entities;

public class Warehouse : Entity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Address { get; set; }
    public Guid LocationId { get; set; }
    public Guid ResponsibleMemberId { get; set; }
    public WarehouseType Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public Location Location { get; set; }
    public Member ResponsibleMember { get; set; }
    public ICollection<WarehouseItem> Items { get; set; }
}

public enum WarehouseType
{
    MainWarehouse = 1,
    RegionalWarehouse = 2,
    TeamWarehouse = 3,
    TemporaryWarehouse = 4
} 