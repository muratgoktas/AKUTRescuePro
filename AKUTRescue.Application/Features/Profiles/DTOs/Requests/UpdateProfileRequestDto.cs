public class UpdateProfileRequestDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePhotoUrl { get; set; }
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