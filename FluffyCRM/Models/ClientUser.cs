using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class ClientUser
    {
        [StringLength(128)]
        public string UserId { get; set; }
        public int?  ClientId  { get; set; }
        public int? ContactId { get; set; }

        [Key]
        public int Id        { get; set; }
    }
}