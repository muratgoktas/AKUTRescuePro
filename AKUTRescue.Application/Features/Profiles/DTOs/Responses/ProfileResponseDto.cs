public class ProfileResponseDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePhotoUrl { get; set; }
    public string TeamName { get; set; }
    public string AuthorityName { get; set; }
    public MemberDetailResponseDto MemberDetail { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}

public class MemberDetailResponseDto
{
    public string IdentityNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BloodType { get; set; }
    public string EmergencyContact { get; set; }
    public string EmergencyPhone { get; set; }
    public string Address { get; set; }
    public string Profession { get; set; }
    public string EducationLevel { get; set; }
    public bool HasFirstAidCertificate { get; set; }
    public DateTime? FirstAidCertificateDate { get; set; }
    public List<string> Certifications { get; set; }
    public List<string> SpecialSkills { get; set; }
} 