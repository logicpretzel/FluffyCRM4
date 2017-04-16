using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class TaskAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? TaskId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }


        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(3)]
        public string Initials { get; set; }

    }

    

}