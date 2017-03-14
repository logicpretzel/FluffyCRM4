using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public enum ticketStatus {
        NONE=0,
        NEW=1,
        ASSIGNED=2,
        STARTED=4,
        HOLD=8,
        COMPLETE=16,
        CLOSED=32
        
    }

    public class Ticket
    {
        [Key]
        [DisplayName("Ticket Number")]
        public int TicketId { get; set; }

        [DisplayName("Subject")]
        [StringLength(255)]
        public string Subject { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }

        [DisplayName("Description")]
        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public ticketStatus? Status { get; set; }

        [DefaultValue(false)]
        public bool DeleteInd { get; set; }

        [DisplayName("Customer")]
        public int? ClientId { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [DisplayName("Date Completed")]
        public DateTime? CompletedDate { get; set; }
        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }
        public DateTime? LocalTime { get; set; }


        [StringLength(128)]
        public string CreatedBy { get; set; }

    }

   
}