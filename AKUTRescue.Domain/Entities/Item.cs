public class Item : Entity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public ItemCategory Category { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public bool RequiresMaintenance { get; set; }
    public int MaintenancePeriod { get; set; } // Gün cinsinden
    public DateTime? LastMaintenanceDate { get; set; }
    public string Specifications { get; set; }
    public string ImageUrl { get; set; }

    // Navigation properties
    public ICollection<WarehouseItem> WarehouseItems { get; set; }
}

public enum ItemCategory
{
    RescueEquipment = 1,
    MedicalSupplies = 2,
    Communication = 3,
    Vehicle = 4,
    Clothing = 5,
    Camp = 6,
    Other = 7
} 