using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    /// <summary>
    /// This ties a user to a client record, and allows the user to be verified.
    /// Also will allow a use to tie to a contact redord so the contact record can be updated
    /// when the use updates their profile.
    /// 
    /// </summary>
    public class ClientUser
    {
        [Required]
        [StringLength(128)]
        [Index("IXUserAndClientId", 1)]
        public string UserId { get; set; }

        [Index("IXUserAndClientId", 2)]
        public int?  ClientId  { get; set; }

        [Index("IXUserAndClientId", 3, IsUnique = true)]
        public int? ContactId { get; set; }
        [StringLength(16)]
        public string VerificationCode { get; set; }

        [DefaultValue(0)]
        public bool Verified { get; set; }

        [Key]
        public int Id        { get; set; }
    }
}