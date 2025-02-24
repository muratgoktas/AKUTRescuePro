public class CreateWarehouseRequestDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Address { get; set; }
    public Guid LocationId { get; set; }
    public Guid ResponsibleMemberId { get; set; }
    public WarehouseType Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
} 