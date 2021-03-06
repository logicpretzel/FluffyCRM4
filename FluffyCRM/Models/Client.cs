﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Display(Name = "Company Name")]
        [MaxLength(150)]
        public string CompanyName { get; set; }

     
        [Display(Name = "Address 1")]
        [MaxLength(150)]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [MaxLength(150)]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [MaxLength(50)]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [MaxLength(10)]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        public FLPhoneTypes? PhoneType1 { get; set; }

     

    }
}