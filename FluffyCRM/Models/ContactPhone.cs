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

    public class ContactPhone
    {
        [StringLength(50)]
        public string Phone { get; set; }
        public FLPhoneTypes PhoneType { get; set; }
        public int ContactId { get; set; }
        [Key]
        public int Id { get; set; }

    }

    public class ClientPhone
    {
        [StringLength(50)]
        public string Phone { get; set; }
        public FLPhoneTypes PhoneType { get; set; }
        public int ClientId { get; set; }

        [Key]
        public int Id { get; set; }

    }



}