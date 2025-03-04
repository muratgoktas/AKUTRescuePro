public class MemberListResponseDto
{
  
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid TeamId { get; set; }
    public string TeamName { get; set; }
    public Guid AuthorityId { get; set; }
    public string AuthorityName { get; set; }
    public bool Status { get; set; }
    public string Barcode { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string CreateByWho { get; set; }
    public string DeletedByWho { get; set; }
    public string UpdatedByWho { get; set; }
} 