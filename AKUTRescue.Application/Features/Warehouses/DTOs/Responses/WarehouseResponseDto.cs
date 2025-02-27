namespace AKUTRescue.Application;
public class WarehouseResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public string ResponsibleMemberName { get; set; }
    public WarehouseType Type { get; set; }
    public int Capacity { get; set; }
    public int CurrentItemCount { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
} 