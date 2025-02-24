
using System;
using System.Collections.Generic;

namespace AKUTRescue.Domain.Entities
{
    public class Location : Entity<Guid>
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Navigation property
        public virtual ICollection<Team> Teams { get; set; }
    }
} 