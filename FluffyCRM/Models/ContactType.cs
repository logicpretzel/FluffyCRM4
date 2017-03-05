using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{

    public enum FLVisibility {
        All=0,
        Client=1,
        Staff=2
    }

    public class ContactType
    {
        [StringLength(50)]
        public string ContactTypeName { get; set; }
        [DefaultValue(0)]
        public FLVisibility Visibility { get; set; }
        public bool delete_ind { get; set; }
        [Key]
        public int Id { get; set; }
    }
}