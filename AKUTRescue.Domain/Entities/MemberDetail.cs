using AKUTRescue.Core.Repositories;
using System;
using System.Collections.Generic;


namespace AKUTRescue.Domain.Entities;

public class MemberDetail : Entity<Guid>
{
    public Guid MemberId { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BloodType { get; set; }
    public string PhoneNumber { get; set; }
    public string EmergencyContact { get; set; }
    public string EmergencyPhone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Profession { get; set; }
    public string EducationLevel { get; set; }
    public bool HasFirstAidCertificate { get; set; }
    public DateTime? FirstAidCertificateDate { get; set; }
    public List<string> Certifications { get; set; }
    public List<string> SpecialSkills { get; set; }

    // Navigation property
    public virtual Member Member { get; set; }
}
