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
    // Navigation properties
    public virtual Authority Authority { get; set; }
    public virtual Team Team { get; set; }
    public virtual MemberDetail MemberDetail { get; set; }
} 