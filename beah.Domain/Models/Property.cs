using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace beah.Domain.Models
{
    public class Property:BaseObject
    {
        [DisplayName("Property Address")]
        public Address Address { get; set; }


        [DisplayName("Site Contact")]
        public string SiteContact { get; set; }


        public string SiteContactNumber { get; set; }
        public string SiteContactEmail { get; set; }

        [DisplayName("Job Status")]
        public PropertyStatus JobStatus { get; set; }

        public virtual ICollection<Party> Caretakers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

        public Party Operator { get; set; }
        public string Instructions { get; set; }
    }

    public enum PropertyStatus
    {
        Occupied,
        Unoccupied,
        Repairs
    }
}