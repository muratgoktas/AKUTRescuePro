
using AKUTRescue.Core.Repositories;
using System;
using System.Collections.Generic;

namespace AKUTRescue.Domain.Entities
{
    public class Authority : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Guid? ParentAuthorityId { get; set; }

        // Navigation properties
        public virtual Authority ParentAuthority { get; set; }
        public virtual ICollection<Authority> SubAuthorities { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
} 