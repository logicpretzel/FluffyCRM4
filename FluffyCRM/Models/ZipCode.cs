using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class ZipCode
    {
        [StringLength(15)]
        public string Zip { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(5)]
        public string StateAbbrev  { get; set; }
        [DefaultValue(1)]
        public int PostalOrder { get; set; }
        [Key]
        public int ID { get; set; }


    }
}