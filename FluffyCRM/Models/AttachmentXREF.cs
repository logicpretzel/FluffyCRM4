using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class AttachmentXREF
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int AttachmentId { get; set; }
    }
}