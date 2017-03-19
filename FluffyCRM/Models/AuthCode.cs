using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class AuthCode
    {
        [Key]
        public int id { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(32)]
        public string Code { get; set; }

        public bool? Valid { get; set; }
        public DateTime? AcceptedDate { get; set; }




    }
}