public class TeamListResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public TeamType Type { get; set; }
    public string Location { get; set; }
    public string TeamLeaderFullName { get; set; }
    public int MemberCount { get; set; }
    public bool Status { get; set; }
} 