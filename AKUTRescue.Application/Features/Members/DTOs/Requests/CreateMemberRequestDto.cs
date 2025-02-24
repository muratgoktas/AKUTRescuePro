public class CreateMemberRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid TeamId { get; set; }
    public Guid AuthorityId { get; set; }
} 