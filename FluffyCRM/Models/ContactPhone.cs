using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public enum FLPhoneTypes {
        None=0,
        Primary=1,
        Secondary=2,
        Home=4,
        Work=8,
        Mobile=16,
        Fax = 32,
        Text = 64,
        Videochat = 128
    }

    public enum FlParentRecType {
        None=0,
        Client=1,
        Contact=2,
        Staff=3

    }

    public class ContactPhone
    {
        [StringLength(50)]
        public string Phone { get; set; }
       
        public FLPhoneTypes PhoneType { get; set; }

        [Required]
        public int ParentId { get; set; }

        public FlParentRecType ParentRecordType { get; set; }
        [StringLength(50)]
        public string Comment { get; set; }
        [Key]
        public int Id { get; set; }

    }




}