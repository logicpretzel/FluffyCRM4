using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class TaskNote
    {
        public enum FlNoteStatus
        {
            NONE = 0,
            NEW = 1,
            OPENED = 2,
            STARTED = 4,
            HOLD = 8,
            COMPLETE = 16,
            CLOSED = 32

        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Note Type")]
        public int? CategoryId { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ParentId { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [DisplayName("Created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        [DefaultValue(0)]
        public FlNoteStatus? Status { get; set; }

        [DefaultValue(false)]
        public bool DeleteInd { get; set; }

        [DisplayName("Customer")]
        public int? ClientId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Completed")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        public DateTime? LocalTime { get; set; }

    }
}