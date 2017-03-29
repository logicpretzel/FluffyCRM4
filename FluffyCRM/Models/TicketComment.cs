using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public enum CommentStatus
    {
        None=0,
        New=1,
        NotifySend=2,
        Seen=3

    }

    public class TicketComment
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Ticket Number")]
        public int TicketId { get; set; }

        [DisplayName("Subject")]
        [StringLength(255)]
        public string Subject { get; set; }

       

        [DisplayName("Description")]
        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

       

       
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }

        public CommentStatus Status { get; set; }

        public DateTime? LocalTime { get; set; }

        [DefaultValue(false)]
        public bool DeleteInd { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }


    }
}