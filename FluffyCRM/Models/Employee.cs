using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(3)]
        public string Initials { get; set; }

        [StringLength(100)]
        public string JobTitle { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [Display(Name = "Address")]
        [MaxLength(150)]
        public string Address { get; set; }
   

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

        [Display(Name = "Phone")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        public FLPhoneTypes? PhoneType2 { get; set; }

    }
}