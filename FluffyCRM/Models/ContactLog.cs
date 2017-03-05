using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public enum FLContactLogTypes {
        None=0,
        Ticket=1,
        Email=2,
        Phone=3,
        Letter=4
    }
    public enum FLActionTypes
    {
        None = 0,
        Escalate=1,
        EnhancementRequest = 2,
        ScheduleUpdate = 3,
        ResearchRequired = 4,
        CallBackRequested=5,
        NeedSalesContact=6,
        CloseTicketRequest=10
    }

    public class ContactLog
    {
        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ContactId { get; set; }

        public FLContactLogTypes ContactLogType { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(8000)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public int TicketId { get; set; }

        public FLActionTypes ActionRequired { get; set; }
        [StringLength(8000)]
        public string ActionComment { get; set; }

        public DateTime CallBackDue { get; set; }

        [StringLength(128)]
        public string RouteToUser { get; set; }

        [Key]
        public int Id { get; set; }
    }
}