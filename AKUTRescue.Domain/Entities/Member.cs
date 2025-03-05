using AKUTRescue.Core.Repositories;
using AKUTRescue.Domain.Entities;
using System;

public class Member : Entity<Guid>
{
    public string UserId { get; set; }
    public string? Barcode { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string FullName => string.IsNullOrEmpty(MiddleName) 
        ? $"{FirstName} {LastName}" 
        : $"{FirstName} {MiddleName} {LastName}";
    public string Email { get; set; }
    public Guid AuthorityId { get; set; }
    public Guid TeamId { get; set; }
    public string ProfilePhotoUrl { get; set; } = "#";
    public Member()
    {
        
    }
    public Member(Guid id, string firstName,string? middleName, string lastName, string email )
    {
        Id = id;
        FirstName = firstName;
        if (middleName != null) { MiddleName = middleName; }
        LastName = lastName;
        FullName = $"{FirstName} {MiddleName} {LastName}";
        Email = email;
    }

    // Navigation properties
    public virtual Authority Authority { get; set; }
    public virtual Team Team { get; set; }
    public virtual MemberDetail MemberDetail { get; set; }
} 