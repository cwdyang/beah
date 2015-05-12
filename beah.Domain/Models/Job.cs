using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace beah.Domain.Models
{
    public class Job:BaseObject
    {
        public virtual ICollection<Property> Properties { get; set; }

        public string PONumber { get; set; }

        [Display(Name = "Job Details")]
        public string Details { get; set; }

        [Display(Name = "Instructions")]
        public string Instructions { get; set; }

        [Display(Name = "Required By")]
        public DateTimeOffset RequiredByDateTime { get; set; }

        public JobStatus JobStatus { get; set; }

        public virtual ICollection<Party> Assignees { get; set; }

        public Party AssignedBy { get; set; }
    }

    public enum JobStatus
    {
        Logged,
        Started,
        WIP,
        Problems,
        Completed,
        Inspected,
        SignedOffInteral,
        SignedOffClient
    }
}
