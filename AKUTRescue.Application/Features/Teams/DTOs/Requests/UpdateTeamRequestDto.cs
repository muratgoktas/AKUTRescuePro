public class UpdateTeamRequestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public TeamType Type { get; set; }
    public Guid LocationId { get; set; }
    public Guid TeamLeaderId { get; set; }
    public Guid? ParentTeamId { get; set; }
    public bool Status { get; set; }
} 