public class WarehouseItem : Entity<Guid>
{
    public Guid WarehouseId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public int MinimumQuantity { get; set; }
    public string Location { get; set; } // Raf/Bölüm bilgisi
    public DateTime? ExpiryDate { get; set; }
    public ItemStatus Status { get; set; }
    public string Notes { get; set; }

    // Navigation properties
    public Warehouse Warehouse { get; set; }
    public Item Item { get; set; }
}

public enum ItemStatus
{
    Available = 1,
    Reserved = 2,
    InMaintenance = 3,
    Damaged = 4,
    Expired = 5
} 