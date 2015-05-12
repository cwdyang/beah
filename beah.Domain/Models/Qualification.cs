using System.Collections.Generic;

namespace beah.Domain.Models
{
    public class Qualification:BaseObject
    {
        //lazy loading 
        //many to many Qualification>Party
        public virtual ICollection<Party> Parties { get; set; }

    }
}