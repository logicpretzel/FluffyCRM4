using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public enum FLCatType { 
        None=0,
        Ticket=1,
        User=2,
        Task=3,
        Project=4,
        Client=5,
        Contact=6,
        Address=7
    }

    public class Category
    {   [Key]
        public int id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        public FLCatType? Type { get; set; }
        public int? delete_ind { get; set; }
        [StringLength(8000)]
        public string Description { get; set; }
        [DisplayName("Create Date")]
        public DateTime? CreateDate { get; set; }
    }
}