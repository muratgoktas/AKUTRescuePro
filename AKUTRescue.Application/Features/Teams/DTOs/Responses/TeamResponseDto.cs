public class TeamResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public TeamType Type { get; set; }
    public string Location { get; set; }
    public string TeamLeaderFullName { get; set; }
    public string ParentTeamName { get; set; }
    public bool Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int MemberCount { get; set; }
} 