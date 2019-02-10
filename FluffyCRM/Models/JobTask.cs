using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class JobTask
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DefaultValue(0)]
        [DisplayName("Task Type")]
        public int? TaskType { get; set; }

        [DefaultValue(0)]
        [DisplayName("Product Name")]
        public int? ProdId { get; set; }

        [DefaultValue(0)]
        [DisplayName("Project Name")]
        public int? ProjectId { get; set; }

      
        [DefaultValue(0)]
        [DisplayName("Parent Task")]
        public int? ParentTaskId { get; set; }

        [ScaffoldColumn(false)]
        [DefaultValue(0)]
        public int? Level { get; set; }

        [DisplayName("Ticket")]
        [DefaultValue(0)]
        public int? TicketId { get; set; }

        [DisplayName("Contact")]
        [StringLength(128)]
        public string ContactUserId { get; set; }

        [DisplayName("Created By")]
        [StringLength(128)]
        public string CreatedBy { get; set; }

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

       
       

    }
}