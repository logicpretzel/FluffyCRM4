using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class Product
    {   [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(8000)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        [StringLength(32)]
        public string CurrentVersion { get; set; }  

    }
}