using System.ComponentModel.DataAnnotations;

namespace beah.Domain.Models
{
    public class Address
    {
        [DataType("decimal")]
        public double Latitude { get; set; }

        [DataType("decimal")]
        public double Longtitude { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string Building { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string Floor { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string Unit { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string StreetNumberName { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string Suburb { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string City { get; set; }

        [StringLength(255)]
        [DataType("string")]
        public string PostCode { get; set; }
    }
}