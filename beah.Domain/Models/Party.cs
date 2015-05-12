using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace beah.Domain.Models
{
    public class Party:BaseObject
    {
        [StringLength(255)]
        [Display(Name = "EntityName")]
        public string EntityName { get; set; }

        [StringLength(255)]
        [Display(Name = "First")]
        public string First { get; set; }

        [StringLength(255)]
        [Display(Name = "Last")]
        public string Last { get; set; }

        //lazy loading 
        //many to many Party>Qualification
        public virtual ICollection<Qualification> Qualifications { get; set; }



        public PartyType PartyType { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public Address Address { get; set; }
    }


    public enum PartyType
    {
        Organisation = 0,
        Admin,
        ClientContact,
        Office,
        Builder,
        HandyMan,
        Electrician,
        Contractor,
        Plumber, 
        Groundsman,
        Maintenance,
        CleanerCommercial,
        CleanerGeneral,
        Operator
    }
}
