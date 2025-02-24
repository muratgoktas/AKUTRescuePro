public class CreateTeamRequestDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public TeamType Type { get; set; }
    public Guid LocationId { get; set; }
    public Guid TeamLeaderId { get; set; }
    public Guid? ParentTeamId { get; set; }
} 